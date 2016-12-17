using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        class DecodedInfo
        {
            internal Type Type { get; }
            internal SqlText Text { get; }
            internal DecodedInfo(Type type, SqlText text)
            {
                Type = type;
                Text = text;
            }
        }

        public SqlConvertingContext Context { get; }

        internal SqlStringConverter(SqlConvertingContext context)
        {
            Context = context;
        }

        public object ToObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj)) throw new NotSupportedException();
            return obj;
        }

        public SqlText Convert(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return Convert(exp).Text;

            var param = obj as DbParam;
            if (param != null) return new ParameterText(null, null, param);

            return new ParameterText(obj);
        }

        DecodedInfo Convert(Expression exp)
        {
            var constant = exp as ConstantExpression;
            if (constant != null) return Convert(constant);

            var newExp = exp as NewExpression;
            if (newExp != null) return Convert(newExp);

            var array = exp as NewArrayExpression;
            if (array != null) return Convert(array);

            var unary = exp as UnaryExpression;
            if (unary != null) return Convert(unary);

            var binary = exp as BinaryExpression;
            if (binary != null) return Convert(binary);

            var member = exp as MemberExpression;
            if (member != null) return Convert(member);

            var method = exp as MethodCallExpression;
            if (method != null) return Convert(method);

            var memberInit = exp as MemberInitExpression;
            if (memberInit != null) return Convert(memberInit);

            throw new NotSupportedException("Not suported expression at LambdicSql.");
        }

        DecodedInfo Convert(MemberInitExpression memberInit)
        {
            var value = ExpressionToObject.GetMemberInitObject(memberInit);
            return new DecodedInfo(memberInit.Type, Convert(value));
        }

        DecodedInfo Convert(ConstantExpression constant)
        {
            //sql syntax.
            if (constant.Type.IsSqlSyntax()) return new DecodedInfo(constant.Type, constant.Value.ToString().ToUpper());

            //normal object.
            if (SupportedTypeSpec.IsSupported(constant.Type)) return new DecodedInfo(constant.Type, Convert(constant.Value));

            throw new NotSupportedException();
        }

        DecodedInfo Convert(NewExpression newExp)
        {
            //syntax.
            if (newExp.Type.IsSqlSyntax())
            {
                //convert expression to text.
                var func = newExp.GetConverotrMethod();
                return new DecodedInfo(null, func(this, newExp));
            }

            //object.
            var value = ExpressionToObject.GetNewObject(newExp);
            return new DecodedInfo(newExp.Type, Convert(value));
        }

        DecodedInfo Convert(NewArrayExpression array)
        {
            if (array.Expressions.Count == 0) return new DecodedInfo(null, string.Empty);
            var infos = array.Expressions.Select(e => Convert(e)).ToArray();
            return new DecodedInfo(infos[0].Type, Arguments(infos.Select(e=>e.Text).ToArray()));
        }

        DecodedInfo Convert(UnaryExpression unary)
        {
            switch (unary.NodeType)
            {
                case ExpressionType.Not:
                    return new DecodedInfo(typeof(bool), Convert(unary.Operand).Text.ConcatAround("NOT (", ")"));
                case ExpressionType.Convert:
                    var ret = Convert(unary.Operand);
                    var p = ret.Text as ParameterText;
                    if (p != null)
                    {
                        if (p.Value != null && !SupportedTypeSpec.IsSupported(p.Value.GetType()))
                        {
                            var casted = ExpressionToObject.ConvertObject(unary.Type, p.Value);
                            return new DecodedInfo(ret.Type, new ParameterText(p.Name, p.MetaId, new DbParam() { Value = casted }));
                        }
                    }
                    return ret;
                //TODO
                case ExpressionType.ArrayLength:
                    throw new NotSupportedException();
                case ExpressionType.ArrayIndex:
                    throw new NotSupportedException();
                default:
                    return Convert(unary.Operand);
            }
        }

        DecodedInfo Convert(BinaryExpression binary)
        {
            var left = Convert(binary.Left);
            var right = Convert(binary.Right);

            if (left.Text.IsEmpty && right.Text.IsEmpty) return new DecodedInfo(null, string.Empty);
            if (left.Text.IsEmpty) return right;
            if (right.Text.IsEmpty) return left;

            //for null
            var nullCheck = ResolveNullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

            var nodeType = Convert(left, binary.NodeType, right);
            return new DecodedInfo(nodeType.Type, new HText(left.Text.ConcatAround("(", ")"), nodeType.Text.ConcatAround(" ", " "), right.Text.ConcatAround("(", ")")));
        }
        
        DecodedInfo Convert(MemberExpression member)
        {
            //sub.Body
            var body = ResolveSqlExpressionBody(member);
            if (body != null) return body;

            //TODO ややこしいから methodとenum以外はやめとこ
            //sql syntax.
            if (member.Member.DeclaringType.IsSqlSyntax())
            {
                //convert.
                return new DecodedInfo(member.Type, member.GetConverotrMethod()(this, member));
            }
            
            //sql syntax extension method
            var method = member.Expression as MethodCallExpression;
            if (method != null && method.Method.DeclaringType.IsSqlSyntax())
            {
                //TODO なんか嫌
                var memberName = method.GetConverotrMethod()(this, new[] { method }).ToString(false, 0, new SqlConvertOption(), new ParameterInfo("")) + "." + member.Member.Name;
                return ResolveLambdicElement(memberName);
            }

            //db element.
            string name;
            if (IsDbDesignParam(member, out name))
            {
                return ResolveLambdicElement(name);
            }

            //get value.
            return ResolveExpressionObject(member);
        }

        private DecodedInfo ResolveLambdicElement(string name)
        {
            TableInfo table;
            if (Context.DbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new DecodedInfo(null, new DbTableText(table));
            }
            ColumnInfo col;
            if (Context.DbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new DecodedInfo(col.Type, new DbColumnText(col));
            }
            return new DecodedInfo(null, name);
        }

        DecodedInfo Convert(MethodCallExpression method)
        {
            //not sql syntax.
            if (!method.Method.DeclaringType.IsSqlSyntax()) return ResolveExpressionObject(method);

            var ret = new List<SqlText>();
            foreach (var c in GetMethodChains(method))
            {
                var chain = c.ToArray();
                ret.Add(chain[0].GetConverotrMethod()(this, c.ToArray()));
            }

            SqlText text = new VText(ret.ToArray());
            if (typeof(SelectClauseText).IsAssignableFrom(ret[0].GetType()))
            {
                text = new SelectQueryText(text);
            }
            else
            {
                text = new QueryText(text);
            }

            return new DecodedInfo(method.Method.ReturnType, text);
        }

        DecodedInfo ResolveSqlExpressionBody(MemberExpression member)
        {
            //get all members.
            var members = new List<MemberExpression>();
            var exp = member;
            while (exp != null)
            {
                members.Add(exp);
                exp = exp.Expression as MemberExpression;
            }
            if (members.Count < 2) return null;
            members.Reverse();

            //check SqlExpression's Body
            if (!typeof(ISqlExpressionBase).IsAssignableFrom(members[0].Type) ||
                members[1].Member.Name != "Body") return null;

            //for example, sub.Body
            if (members.Count == 2) return ResolveExpressionObject(members[0]);

            //for example, sub.Body.column.
            else return new DecodedInfo(member.Type, string.Join(".", members.Where((e, i) => i != 1).Select(e => e.Member.Name).ToArray()));
        }

        DecodedInfo Convert(DecodedInfo left, ExpressionType nodeType, DecodedInfo right)
        {
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
                            return new DecodedInfo(left.Type, new StringAddOperatorText());
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

            var leftParam = left.Text as ParameterText;
            var rightParam = right.Text as ParameterText;

            var leftObj = leftParam != null ? leftParam.Value : null;
            var rightObj = rightParam != null ? rightParam.Value : null;
            var bothParam = (leftParam != null && rightParam != null);

            var isParams = new[] { leftParam != null, rightParam != null };
            var objs = new[] { leftObj, rightObj };
            var names = new[] { left.Text, right.Text };
            var targetTexts = new[] { right.Text, left.Text };
            for (int i = 0; i < isParams.Length; i++)
            {
                var obj = objs[i];
                if (isParams[i])
                {
                    var nullObj = obj == null;
                    if (!nullObj)
                    {
                        if (bothParam) continue;
                        return null;
                    }
                    return new DecodedInfo(null, targetTexts[i].ConcatAround("(", ")" + ope));
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
                    //using ParameterExpression with LambdicSql only when it represents a component of db.
                    //for example, Sql<DB>.Create(db =>
                    lambdaName = string.Join(".", names.ToArray());
                    return true;
                }
                member = member.Expression as MemberExpression;
            }
            return false;
        }

        DecodedInfo ResolveExpressionObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj))
            {
                throw new NotSupportedException();
            }

            //array.
            if (obj != null && obj.GetType().IsArray)
            {
                var list = new List<SqlText>();
                foreach (var e in (IEnumerable)obj)
                {
                    list.Add(Convert(e));
                }
                return new DecodedInfo(exp.Type, Arguments(list.ToArray()));
            }

            //value type is SqlSyntax
            //for example [ enum ]
            if (exp.Type.IsSqlSyntax())
            {
                return new DecodedInfo(exp.Type, obj.ToString().ToUpper());
            }

            //normal object.
            if (SupportedTypeSpec.IsSupported(exp.Type))
            {
                string name = string.Empty;
                MetaId metaId = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                    metaId = new MetaId(member.Member);
                }

                //use field name.
                return new DecodedInfo(exp.Type, new ParameterText(name, metaId, new DbParam() { Value = metaId }));
            }

            //DbParam.
            if (typeof(DbParam).IsAssignableFrom(exp.Type))
            {
                string name = string.Empty;
                MetaId metaId = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                    metaId = new MetaId(member.Member);
                }
                var param = ((DbParam)obj);
                //use field name.
                return new DecodedInfo(exp.Type.GetGenericArguments()[0], new ParameterText(name, metaId, param));
            }
            
            //SqlExpression.
            //example [ from(exp) ]
            var sqlExp = obj as ISqlExpressionBase;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new DecodedInfo(type,sqlExp.Convert(this));
            }

            //others.
            //If it is correctly written it will be cast at the caller.
            {
                string name = string.Empty;
                MetaId metaId = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                    metaId = new MetaId(member.Member);
                }

                //use field name.
                return new DecodedInfo(exp.Type, new ParameterText(name, metaId, new DbParam() { Value = obj }));
            }
        }

        static List<List<MethodCallExpression>> GetMethodChains(MethodCallExpression end)
        {
            //resolve chain.
            if (end.Method.CanResolveSqlSyntaxMethodChain())
            {
                return new List<List<MethodCallExpression>> { new List<MethodCallExpression> { end } };
            }

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
                    bool isSqlSyntax = curent.Method.IsDefined(typeof(ExtensionAttribute), false) && 0 < ps.Length && typeof(IMethodChain).IsAssignableFrom(ps[0].ParameterType);
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
