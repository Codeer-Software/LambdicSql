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
    class ExpressionConverter : IExpressionConverter
    {
        class DecodedInfo
        {
            internal Type Type { get; }
            internal ExpressionElement Text { get; }
            internal DecodedInfo(Type type, ExpressionElement text)
            {
                Type = type;
                Text = text;
            }
        }
        
        public DbInfo DbInfo { get; }

        internal ExpressionConverter(DbInfo info)
        {
            DbInfo = info;
        }

        public object ToObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj)) throw new NotSupportedException();
            return obj;
        }

        public ExpressionElement Convert(object obj)
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
            var cnv = constant.Type.GetSqlSyntaxObject();
            if (cnv != null) return new DecodedInfo(constant.Type, cnv.Convert(constant.Value));

            //normal object.
            if (SupportedTypeSpec.IsSupported(constant.Type)) return new DecodedInfo(constant.Type, Convert(constant.Value));

            throw new NotSupportedException();
        }

        DecodedInfo Convert(NewExpression newExp)
        {
            //syntax.
            var cnv = newExp.GetSqlSyntaxNew();
            if (cnv != null)
            {
                return new DecodedInfo(null, cnv.Convert(this, newExp));
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

        //TODO refactoring.
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
                case ExpressionType.ArrayLength:
                    {
                        object obj;
                        ExpressionToObject.GetExpressionObject(unary.Operand, out obj);
                        return new DecodedInfo(typeof(int), new ParameterText(((Array)obj).Length));
                    }
                default:
                    return Convert(unary.Operand);
            }
        }

        DecodedInfo Convert(BinaryExpression binary)
        {
            if (binary.NodeType == ExpressionType.ArrayIndex)
            {
                object ary;
                ExpressionToObject.GetExpressionObject(binary.Left, out ary);
                object index;
                ExpressionToObject.GetExpressionObject(binary.Right, out index);
                return new DecodedInfo(typeof(int), new ParameterText(((Array)ary).GetValue((int)index)));
            }

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

            //sql syntax.
            var cnvMember = member.GetSqlSyntaxMember();
            if (cnvMember != null)
            {
                //convert.
                return new DecodedInfo(member.Type, cnvMember.Convert(this, member));
            }
            
            //sql syntax extension method
            var method = member.Expression as MethodCallExpression;
            if (method != null)
            {
                var cnv = method.GetSqlSyntaxMethod();
                if (cnv != null)
                {
                    var ret = cnv.Convert(this, method);
                    //T()
                    var tbl = ret as DbTableText;
                    if (tbl != null)
                    {
                        var memberName = tbl.Info.LambdaFullName + "." + member.Member.Name;
                        return ResolveLambdicElement(memberName);
                    }
                    throw new NotSupportedException();
                }
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
            if (DbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new DecodedInfo(null, new DbTableText(table));
            }
            ColumnInfo col;
            if (DbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new DecodedInfo(col.Type, new DbColumnText(col));
            }
            return new DecodedInfo(null, name);
        }

        //TODO
        DecodedInfo Convert(MethodCallExpression method)
        {
            //not sql syntax.
            if (method.GetSqlSyntaxMethod() == null) return ResolveExpressionObject(method);

            var ret = new List<ExpressionElement>();
            foreach (var c in GetMethodChains(method))
            {
                ret.Add(c.GetSqlSyntaxMethod().Convert(this, c));
            }
            //TODO ちょっと嫌すぎる。括弧を付けない方法を何か確立せねば
            if (ret.Count == 1 && typeof(SqlSyntaxAllAttribute.DisableBracketsText).IsAssignableFrom(ret[0].GetType()))
            {
                return new DecodedInfo(method.Method.ReturnType, ret[0]);
            }


            ExpressionElement text = new VText(ret.ToArray());
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
                if (exp != null)
                {
                    member = exp;
                }
            }

            //check IClauseChain's Body.
            var method = member.Expression as MethodCallExpression;
            if (method != null)
            {
                if (!typeof(IClauseChain).IsAssignableFrom(method.Type) ||
                     member.Member.Name != "Body") return null;
                return Convert(method);
            }

            if (members.Count < 2) return null;
            members.Reverse();
            
            //check SqlExpression's Body
            if (!typeof(ISqlExpression).IsAssignableFrom(members[0].Type) ||
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
                var list = new List<ExpressionElement>();
                foreach (var e in (IEnumerable)obj)
                {
                    list.Add(Convert(e));
                }
                return new DecodedInfo(exp.Type, Arguments(list.ToArray()));
            }

            //value type is SqlSyntax
            //for example [ enum ]
            var cnv = exp.Type.GetSqlSyntaxObject();
            if (cnv != null) 
            {
                return new DecodedInfo(exp.Type, cnv.Convert(obj));
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
                return new DecodedInfo(exp.Type, new ParameterText(name, metaId, new DbParam() { Value = obj }));
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
            var sqlExp = obj as ISqlExpression;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new DecodedInfo(type, sqlExp.ExpressionElement);
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

        //TODO refactoring.
        static List<MethodCallExpression> GetMethodChains(MethodCallExpression end)
        {
            var chains = new List<MethodCallExpression>();
            var curent = end;
            while (curent != null && curent.GetSqlSyntaxMethod() != null)
            {
                chains.Add(curent);
                
                if (!curent.Method.IsDefined(typeof(ExtensionAttribute), false)) break;

                var next = (0 < curent.Arguments.Count) ? curent.Arguments[0] as MethodCallExpression : null;
                curent = next;
            }
            chains.Reverse();
            return chains;
        }
    }
}
