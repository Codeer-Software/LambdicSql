using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        ISqlStringConverterCustomizer _queryCustomizer;

        public DecodeContext Context { get; }

        internal SqlStringConverter(DecodeContext context, ISqlStringConverterCustomizer queryCustomizer)
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
            var constant = exp as ConstantExpression;
            if (constant != null) return ToString(constant);

            var newExp = exp as NewExpression;
            if (newExp != null) return ToString(newExp);

            var array = exp as NewArrayExpression;
            if (array != null) return ToString(array);

            var unary = exp as UnaryExpression;
            if (unary != null) return ToString(unary);

            var binary = exp as BinaryExpression;
            if (binary != null) return ToString(binary);

            var member = exp as MemberExpression;
            if (member != null) return ToString(member);

            var method = exp as MethodCallExpression;
            if (method != null) return ToString(method);

            throw new NotSupportedException("Not suported expression at LambdicSql.");
        }

        DecodedInfo ToString(ConstantExpression constant)
        {
            if (constant.Type.IsSqlSyntax()) return new DecodedInfo(constant.Type, constant.Value.ToString());
            if (SupportedTypeSpec.IsSupported(constant.Type)) return new DecodedInfo(constant.Type, ToString(constant.Value));
            throw new NotSupportedException();
        }

        DecodedInfo ToString(NewExpression newExp)
        {
            if (newExp.Type.IsSqlSyntax())
            {
                var func = newExp.GetNewToString();
                return new DecodedInfo(null, func(this, newExp));
            }
            object value;
            if (ExpressionToObject.GetNewObject(newExp, out value)) return new DecodedInfo(newExp.Type, ToString(value));
            throw new NotSupportedException();
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

        DecodedInfo ToString(MemberExpression member)
        {
            //SubQuery's member.
            //example [ sub.Cast().id ]
            var method = member.Expression as MethodCallExpression;
            if (method != null && method.Method.DeclaringType.IsSqlSyntax())
            {
                if (method.Method.Name != "Cast") throw new NotSupportedException();
                var mem2 = method.Arguments[0] as MemberExpression;
                return new DecodedInfo(null, mem2.Member.Name + "." + member.Member.Name);
            }
            
            //TODO db =>のdbからの要素であること
            //TODO ★Expressionが異なると、DbInfoも異なる。これ対応できてない！
            //db element.
            string name = GetMemberCheckName(member);
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

            //get value.
            return ResolveExpressionObject(member);
        }

        DecodedInfo ToString(MethodCallExpression method)
        {
            if (!method.Method.DeclaringType.IsSqlSyntax())
            {
                return ResolveExpressionObject(method);
            }

            //ISqlExpression extensions.
            if (typeof(ISqlExpression).IsAssignableFrom(method.Method.GetParameters()[0].ParameterType))
            {
                if (method.Method.Name != "Cast") throw new NotSupportedException();
                return ResolveExpressionObject(method.Arguments[0]);
            }

            var ret = new List<string>();
            foreach (var c in GetMethodChains(method))
            {
                var chain = c.ToArray();

                //custom.
                if (_queryCustomizer != null)
                {
                    var custom = _queryCustomizer.CusotmMethodsToString(this, chain);
                    if (custom != null)
                    {
                        ret.Add(custom);
                        continue;
                    }
                }

                //normal.
                ret.Add(chain[0].GetMethodsToString()(this, chain));
            }

            //Cast for IMethodChain.
            var text = string.Join(string.Empty, ret.ToArray());
            if (method.Method.Name == "Cast") text = AdjustSubQueryString(text);
            return new DecodedInfo(method.Method.ReturnType, text);
        }

        DecodedInfo ResolveExpressionObject(Expression exp)
        {
            //TODO member.GetFunc().idの対応

            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj)) return null;

            //value type is SqlSyntax
            //example [ enum ]
            if (exp.Type.IsSqlSyntax())
            {
                return new DecodedInfo(exp.Type, obj.ToString());
            }

            //null → IS NULL, IS NOT NULL
            if (obj == null)
            {
                return new DecodedInfo(exp.Type, ToString((object)null));
            }

            if (SupportedTypeSpec.IsSupported(exp.Type))
            {
                string name = string.Empty;
                int? metadataToken = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = GetMemberCheckName(member);
                    metadataToken = member.Member.MetadataToken;
                }

                //use field name.
                return new DecodedInfo(obj.GetType(), Context.Parameters.Push(name, metadataToken, obj));
            }

            //SqlExpression.
            //example [ from(exp) ]
            var sqlExp = obj as ISqlExpression;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new DecodedInfo(type, AdjustSubQueryString(sqlExp.ToString(this)));
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

            //TODO Function().value
            //この場合は、名前を付けてはならない。

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
                var group = new List<MethodCallExpression>();
                while (true)
                {
                    group.Add(curent);
                    var ps = curent.Method.GetParameters();
                    bool isGrouping = 0 < ps.Length && typeof(IMethodChainGroup).IsAssignableFrom(ps[0].ParameterType);
                    bool isSqlSyntax = 0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType);
                    var next = isSqlSyntax ? curent.Arguments[0] as MethodCallExpression : null;

                    //end of syntax
                    if (next == null)
                    {
                        group.Reverse();
                        chains.Add(group);
                        chains.Reverse();
                        return chains;
                    }

                    curent = next;

                    //end of chain
                    if (!isGrouping)
                    {
                        break;
                    }
                }
                group.Reverse();
                chains.Add(group);
            }
        }
    }
}
