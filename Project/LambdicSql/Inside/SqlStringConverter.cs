using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        SqlConvertOption _option;
        ISqlSyntaxCustomizer _sqlSyntaxCustomizer;

        public DecodeContext Context { get; }

        internal SqlStringConverter(DecodeContext context, SqlConvertOption option, ISqlSyntaxCustomizer sqlSyntaxCustomizer)
        {
            Context = context;
            _option = option;
            _sqlSyntaxCustomizer = sqlSyntaxCustomizer;
        }

        public object ToObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj)) throw new NotSupportedException();
            return obj;
        }

        public string ToString(object obj)
        {
            if (obj == null) return "NULL";

            var exp = obj as Expression;
            if (exp != null) return ToString(exp).Text;

            var param = obj as DbParam;
            if (param != null) return Context.Parameters.Push(param.Value, null, null, param);

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

            var memberInit = exp as MemberInitExpression;
            if (memberInit != null) return ToString(memberInit);

            throw new NotSupportedException("Not suported expression at LambdicSql.");
        }

        DecodedInfo ToString(MemberInitExpression memberInit)
        {
            object value;
            if (ExpressionToObject.GetMemberInitObject(memberInit, out value)) return new DecodedInfo(memberInit.Type, ToString(value));
            throw new NotSupportedException();
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
                if (_sqlSyntaxCustomizer != null)
                {
                    var ret = _sqlSyntaxCustomizer.ToString(this, newExp);
                    if (ret != null) return new DecodedInfo(null, ret);
                }
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
            if (member.Member.DeclaringType.IsSqlSyntax())
            {
                if (_sqlSyntaxCustomizer != null)
                {
                    var custom = _sqlSyntaxCustomizer.ToString(this, member);
                    if (custom != null)
                    {
                        return new DecodedInfo(member.Type, custom);
                    }
                }
                return new DecodedInfo(member.Type, member.GetMemberToString()(this, member));
            }

            //SubQuery's member.
            //example [ sub.Cast().id ]
            var method = member.Expression as MethodCallExpression;
            if (method != null && method.Method.DeclaringType.IsSqlSyntax())
            {
                var text = method.GetMethodsToString()(this, new[] { method });
                if (string.IsNullOrEmpty(text)) text = ".";
                else text = "." + text + ".";

                var mem2 = method.Arguments[0] as MemberExpression;
                return new DecodedInfo(null, mem2.Member.Name + text + member.Member.Name);
            }

            //db element.
            string name;
            if (IsDbDesignParam(member, out name))
            {
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
                throw new NotSupportedException();
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
            var args = method.Method.GetParameters();
            if (0 < args.Length && typeof(ISqlExpression).IsAssignableFrom(args[0].ParameterType))
            {
                if (method.Method.Name == "Cast" || method.Method.Name == "T") return ResolveExpressionObject(method.Arguments[0]);
            }

            var ret = new List<string>();
            foreach (var c in GetMethodChains(method))
            {
                var chain = c.ToArray();

                //custom.
                if (_sqlSyntaxCustomizer != null)
                {
                    var custom = _sqlSyntaxCustomizer.ToString(this, chain);
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
            if (method.Method.Name == "Cast" || method.Method.Name == "T") text = AdjustSubQueryString(text);
            return new DecodedInfo(method.Method.ReturnType, text);
        }

        DecodedInfo ResolveExpressionObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj))
            {
                throw new NotSupportedException();
            }

            //value type is SqlSyntax
            //example [ enum ]
            if (exp.Type.IsSqlSyntax())
            {
                if (_sqlSyntaxCustomizer != null)
                {
                    var ret = _sqlSyntaxCustomizer.ToString(this, obj);
                    if (ret != null) return new DecodedInfo(exp.Type, obj.ToString());
                }
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
                    name = member.Member.Name;
                    metadataToken = member.Member.MetadataToken;
                }

                //use field name.
                return new DecodedInfo(exp.Type, Context.Parameters.Push(obj, name, metadataToken));
            }

            //TODO refactoring.
            if (typeof(DbParam).IsAssignableFrom(exp.Type))
            {
                string name = string.Empty;
                int? metadataToken = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                    metadataToken = member.Member.MetadataToken;
                }
                var param = ((DbParam)obj);
                obj = param.Value;
                //use field name.
                return new DecodedInfo(exp.Type.GetGenericArguments()[0], Context.Parameters.Push(obj, name, metadataToken, param));
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
            //_option
            switch (nodeType)
            {
                case ExpressionType.Equal: return new DecodedInfo(typeof(bool), "=");
                case ExpressionType.NotEqual: return new DecodedInfo(typeof(bool), "<>");
                case ExpressionType.LessThan: return new DecodedInfo(typeof(bool), "<");
                case ExpressionType.LessThanOrEqual: return new DecodedInfo(typeof(bool), "<=");
                case ExpressionType.GreaterThan: return new DecodedInfo(typeof(bool), ">");
                case ExpressionType.GreaterThanOrEqual: return new DecodedInfo(typeof(bool), ">=");
                case ExpressionType.Add:
                    {
                        if (left.Type == typeof(string) || right.Type == typeof(string))
                        {
                            return new DecodedInfo(left.Type, _option.StringAddOperator);
                        }
                        return new DecodedInfo(left.Type, "+");
                    }
                case ExpressionType.Subtract: return new DecodedInfo(left.Type, "-");
                case ExpressionType.Multiply: return new DecodedInfo(left.Type, "*");
                case ExpressionType.Divide: return new DecodedInfo(left.Type, "/");
                case ExpressionType.Modulo: return new DecodedInfo(left.Type, "%");
                case ExpressionType.And: return new DecodedInfo(typeof(bool), "AND");
                case ExpressionType.AndAlso: return new DecodedInfo(typeof(bool), "AND");
                case ExpressionType.Or: return new DecodedInfo(typeof(bool), "OR");
                case ExpressionType.OrElse: return new DecodedInfo(typeof(bool), "OR");
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

        static bool IsDbDesignParam(MemberExpression member, out string lambdaName)
        {
            lambdaName = string.Empty;
            var names = new List<string>();
            while (member != null)
            {
                names.Insert(0, member.Member.Name);
                if (member.Expression is ParameterExpression)
                {
                    lambdaName = string.Join(".", names.ToArray());
                    return true;
                }
                member = member.Expression as MemberExpression;
            }
            return false;
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

        //TODO refactoring.
        static List<List<MethodCallExpression>> GetMethodChains(MethodCallExpression end)
        {
            var chains = new List<List<MethodCallExpression>>();
            var curent = end;
            while (true)
            {
                var type = curent.Method.DeclaringType;
                var group = new List<MethodCallExpression>();
                string groupName = string.Empty;
                while (true)
                {
                    var oldGroupName = groupName;
                    var currentGroupName = curent.Method.GetMethodGroupName();
                    groupName = currentGroupName;
                    if (!string.IsNullOrEmpty(oldGroupName) && oldGroupName != currentGroupName)
                    {
                        groupName = string.Empty;
                        break;
                    }

                    group.Add(curent);
                    var ps = curent.Method.GetParameters();
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
                    if (string.IsNullOrEmpty(currentGroupName))
                    {
                        groupName = string.Empty;
                        break;
                    }
                }
                group.Reverse();
                chains.Add(group);
            }
        }
    }
}
