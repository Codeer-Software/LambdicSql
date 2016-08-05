using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        IQueryCustomizer _queryCustomizer;

        public DecodeContext Context { get; }

        internal SqlStringConverter(DecodeContext context, IQueryCustomizer queryCustomizer)
        {
            Context = context;
            _queryCustomizer = queryCustomizer;
        }

        public string ToString(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return ToString(exp).Text;
            return Context.Parameters.Push(obj);
        }

        DecodedInfo ToString(Expression exp)
        {
            var member = exp as MemberExpression;
            if (member != null) return ToString(member);

            var constant = exp as ConstantExpression;
            if (constant != null) return ToString(constant);

            var binary = exp as BinaryExpression;
            if (binary != null) return ToString(binary);

            var method = exp as MethodCallExpression;
            if (method != null) return ToString(method);

            var unary = exp as UnaryExpression;
            if (unary != null) return ToString(unary);

            var array = exp as NewArrayExpression;
            if (array != null) return ToString(array);

            throw new NotSupportedException("Not suported expression at LambdicSql.");
        }

        DecodedInfo ToString(NewArrayExpression array)
        {
            if (array.Expressions.Count == 0) return new DecodedInfo(null, string.Empty);
            var infos = array.Expressions.Select(e => ToString(e)).ToArray();
            return new DecodedInfo(infos[0].Type, string.Join(", ", infos.Select(e => e.Text).ToArray()));
        }

        DecodedInfo ToString(UnaryExpression unary)
            => unary.NodeType == ExpressionType.Not ?
                new DecodedInfo(typeof(bool), "NOT (" + ToString(unary.Operand) + ")") :
                ToString(unary.Operand);

        DecodedInfo ToString(MethodCallExpression method)
        {
            if (IsSqlExpressionCast(method)) return ResolveSqlExpressionCast(method);
            if (IsSqlSyntaxResolver(method)) return ResolveSqlSyntax(method);
            object value;
            if (ExpressionToObject.GetMethodObject(method, out value)) return new DecodedInfo(method.Method.ReturnType, ToString(value));
            throw new NotSupportedException("Can't use normal functions.");
        }

        DecodedInfo ToString(BinaryExpression binary)
        {
            var left = ToString(binary.Left);
            var right = ToString(binary.Right);

            if (string.IsNullOrEmpty(left.Text) && string.IsNullOrEmpty(right.Text)) return new DecodedInfo(null, string.Empty);
            if (string.IsNullOrEmpty(left.Text)) return right;
            if (string.IsNullOrEmpty(right.Text)) return left;

            //for null
            var nullCheck = ResolveNullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

            var nodeType = ToString(left, binary.NodeType, right);
            return new DecodedInfo(nodeType.Type, "(" + left.Text + ") " + nodeType.Text + " (" + right.Text + ")");
        }

        DecodedInfo ToString(ConstantExpression constant)
        {
            if (constant.Value == null) return new DecodedInfo(null, ToString((object)null));
            var type = constant.Value.GetType();
            if (SupportedTypeSpec.IsSupported(type)) return new DecodedInfo(type, ToString(constant.Value));
            if (type.IsEnum) return new DecodedInfo(type, constant.Value.ToString());

            throw new NotSupportedException();
        }

        DecodedInfo ToString(MemberExpression member)
        {
            string name = GetMemberCheckName(member);
            
            //db element.
            TableInfo table;
            if (Context.DbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new DecodedInfo(null, table.SqlFullName);
            }
            ColumnInfo col;
            if (Context.DbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new DecodedInfo(col.Type, col.SqlFullName);
            }

            //SubQuery's member.
            //exsample [sub.id]
            var method = member.Expression as MethodCallExpression;
            if (method != null && IsSqlExpressionCast(method))
            {
                var mem2 = method.Arguments[0] as MemberExpression;
                return new DecodedInfo(null, mem2.Member.Name + "." + name);
            }

            //value.
            object obj;
            if (!ExpressionToObject.GetMemberObject(member, out obj)) return null;

            if (obj == null) return new DecodedInfo(null, ToString((object)null));
            if (SupportedTypeSpec.IsSupported(obj.GetType()))
            {
                //use field name.
                return new DecodedInfo(obj.GetType(), Context.Parameters.Push(name, member.Member.MetadataToken, obj));
            }

            //SqlExpression.
            var sqlExp = obj as ISqlExpression;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new DecodedInfo(type, sqlExp.ToString(this));
            }

            throw new NotSupportedException("Invalid object.");
        }

        DecodedInfo ToString(DecodedInfo left, ExpressionType nodeType, DecodedInfo right)
        {
            Func<string, string> custom = @operator => _queryCustomizer == null ? @operator : _queryCustomizer.CustomOperator(left.Type, @operator, right.Type);
            switch (nodeType)
            {
                case ExpressionType.Equal: return new DecodedInfo(typeof(bool), custom("="));
                case ExpressionType.NotEqual: return new DecodedInfo(typeof(bool), custom("<>"));
                case ExpressionType.LessThan: return new DecodedInfo(typeof(bool), custom("<"));
                case ExpressionType.LessThanOrEqual: return new DecodedInfo(typeof(bool), custom("<="));
                case ExpressionType.GreaterThan: return new DecodedInfo(typeof(bool), custom(">"));
                case ExpressionType.GreaterThanOrEqual: return new DecodedInfo(typeof(bool), custom(">="));
                case ExpressionType.Add: return new DecodedInfo(left.Type, custom("+"));
                case ExpressionType.Subtract: return new DecodedInfo(left.Type, custom("-"));
                case ExpressionType.Multiply: return new DecodedInfo(left.Type, custom("*"));
                case ExpressionType.Divide: return new DecodedInfo(left.Type, custom("/"));
                case ExpressionType.Modulo: return new DecodedInfo(left.Type, custom("%"));
                case ExpressionType.And: return new DecodedInfo(typeof(bool), custom("AND"));
                case ExpressionType.AndAlso: return new DecodedInfo(typeof(bool), custom("AND"));
                case ExpressionType.Or: return new DecodedInfo(typeof(bool), custom("OR"));
                case ExpressionType.OrElse: return new DecodedInfo(typeof(bool), custom("OR"));
            }
            throw new NotImplementedException();
        }

        DecodedInfo ResolveSqlExpressionCast(MethodCallExpression method)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(method.Arguments[0], out obj))
            {
                throw new NotSupportedException();
            }
            var text = ((ISqlExpression)obj).ToString(this);
            text = AdjustSubQueryString(text);
            return new DecodedInfo(typeof(ISqlExpression), text);
        }

        DecodedInfo ResolveSqlSyntax(MethodCallExpression method)
        {
            var ret = new List<string>();
            foreach (var c in GetMethodChains(method))
            {
                var chain = c.ToArray();

                //custom.
                if (_queryCustomizer != null)
                {
                    var custom = _queryCustomizer.CusotmSqlSyntax(this, chain);
                    if (custom != null)
                    {
                        ret.Add(custom);
                        continue;
                    }
                }

                //normal.
                ret.Add(chain[0].Method.GetMethodsToString()(this, chain));
            }

            var text = string.Join(string.Empty, ret.ToArray());
            if (IsSqlKeyWordCast(method)) text = AdjustSubQueryString(text);
            return new DecodedInfo(method.Method.ReturnType, text);
        }

        DecodedInfo ResolveNullCheck(DecodedInfo left, ExpressionType nodeType, DecodedInfo right)
        {
            string ope;
            switch (nodeType)
            {
                case ExpressionType.Equal: ope = " IS NULL"; break;
                case ExpressionType.NotEqual: ope = " IS NOT NULL"; break;
                default: return null;
            }

            object leftObj, rightObj;
            var leftIsParam = Context.Parameters.TryGetParam(left.Text, out leftObj);
            var rightIsParam = Context.Parameters.TryGetParam(right.Text, out rightObj);
            if (leftIsParam && rightIsParam) return null;

            var isParams = new[] { leftIsParam, rightIsParam };
            var objs = new[] { leftObj, rightObj };
            var names = new[] { left.Text, right.Text };
            var targetTexts = new[] { right.Text, left.Text };
            for (int i = 0; i < isParams.Length; i++)
            {
                var obj = objs[i];
                if (isParams[i])
                {
                    var nullObj = obj == null;
                    if (!nullObj) return null;
                    Context.Parameters.Remove(names[i]);
                    return new DecodedInfo(null, "(" + targetTexts[i] + ")" + ope);
                }
            }
            return null;
        }

        static string GetMemberCheckName(MemberExpression member)
        {
            var names = new List<string>();
            var checkName = member;
            while (checkName != null)
            {
                names.Insert(0, checkName.Member.Name);
                checkName = checkName.Expression as MemberExpression;
            }
            var name = string.Join(".", names.ToArray());
            return name;
        }

        static bool IsSqlSyntaxResolver(MethodCallExpression method)
            => method.Method.IsStatic &&
                0 < method.Arguments.Count &&
                typeof(ISqlSyntax).IsAssignableFrom(method.Arguments[0].Type);

        static bool IsSqlExpressionCast(MethodCallExpression method)
            => method.Method.DeclaringType == typeof(SqlExpressionExtensions) &&
               method.Method.Name == nameof(SqlExpressionExtensions.Cast);

        static bool IsSqlKeyWordCast(MethodCallExpression method)
            => method.Method.DeclaringType == typeof(SqlKeyWordExtensions) &&
               method.Method.Name == nameof(SqlKeyWordExtensions.Cast);

        static string AdjustSubQueryString(string text)
        {
            if (text.Replace(Environment.NewLine, string.Empty).Trim().IndexOf("SELECT") != 0) return text;

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            lines[0] = InsertSubQueryStart(lines[0]);
            return Environment.NewLine + string.Join(Environment.NewLine, lines.Select(e => "\t" + e).ToArray()) + ")";
        }

        static string InsertSubQueryStart(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '\t')
                {
                    return line.Substring(0, i) + "(" + line.Substring(i);
                }
            }
            return line;
        }

        static List<List<MethodCallExpression>> GetMethodChains(MethodCallExpression end)
        {
            var chains = new List<List<MethodCallExpression>>();
            var curent = end;
            while (true)
            {
                var type = curent.Method.DeclaringType;
                var chain = new List<MethodCallExpression>();
                while (true)
                {
                    chain.Add(curent);
                    var next = curent.Arguments[0] as MethodCallExpression;
                    if (next == null)
                    {
                        chain.Reverse();
                        chains.Add(chain);
                        chains.Reverse();
                        return chains;
                    }
                    curent = next;
                    if (next.Method.DeclaringType != type)
                    {
                        break;
                    }
                }
                chain.Reverse();
                chains.Add(chain);
            }
        }
    }
}
