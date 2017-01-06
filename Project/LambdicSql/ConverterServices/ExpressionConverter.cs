using LambdicSql.ConverterServices.SymbolConverters.Inside;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public class ExpressionConverter
    {
        class ConvertedResult
        {
            internal Type Type { get; }
            internal Parts Text { get; }
            internal ConvertedResult(Type type, Parts text)
            {
                Type = type;
                Text = text;
            }
        }

        DbInfo DbInfo { get; }

        internal ExpressionConverter(DbInfo info)
        {
            DbInfo = info;
        }

        /// <summary>
        /// Get object in expression.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>object.</returns>
        public object ToObject(Expression expression)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(expression, out obj)) throw new NotSupportedException();
            return obj;
        }

        /// <summary>
        /// Convert object to sql text.
        /// </summary>
        /// <param name="obj">object.</param>
        /// <returns>text.</returns>
        public Parts Convert(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return Convert(exp).Text;

            var param = obj as DbParam;
            if (param != null) return new ParameterParts(null, null, param);

            return new ParameterParts(obj);
        }

        ConvertedResult Convert(Expression exp)
        {
            var method = exp as MethodCallExpression;
            if (method != null) return Convert(method);

            var constant = exp as ConstantExpression;
            if (constant != null) return Convert(constant);

            var binary = exp as BinaryExpression;
            if (binary != null) return Convert(binary);

            var unary = exp as UnaryExpression;
            if (unary != null) return Convert(unary);

            var member = exp as MemberExpression;
            if (member != null) return Convert(member);

            var newExp = exp as NewExpression;
            if (newExp != null) return Convert(newExp);

            var array = exp as NewArrayExpression;
            if (array != null) return Convert(array);

            var memberInit = exp as MemberInitExpression;
            if (memberInit != null) return Convert(memberInit);

            throw new NotSupportedException("Its way of writing is not supported by LambdicSql.");
        }

        ConvertedResult Convert(MemberInitExpression memberInit)
        {
            var value = ExpressionToObject.GetMemberInitObject(memberInit);
            return new ConvertedResult(memberInit.Type, Convert(value));
        }

        ConvertedResult Convert(ConstantExpression constant)
        {
            //sql symbol.
            var symbol = constant.Type.GetObjectConverter();
            if (symbol != null) return new ConvertedResult(constant.Type, symbol.Convert(constant.Value));

            //normal object.
            return new ConvertedResult(constant.Type, Convert(constant.Value));
        }

        ConvertedResult Convert(NewExpression newExp)
        {
            //symbol.
            var symbol = newExp.GetNewConverter();
            if (symbol != null) return new ConvertedResult(null, symbol.Convert(newExp, this));

            //object.
            var value = ExpressionToObject.GetNewObject(newExp);
            return new ConvertedResult(newExp.Type, Convert(value));
        }

        ConvertedResult Convert(NewArrayExpression array)
        {
            if (array.Expressions.Count == 0) return new ConvertedResult(null, string.Empty);
            var infos = array.Expressions.Select(e => Convert(e)).ToArray();
            return new ConvertedResult(infos[0].Type, Arguments(infos.Select(e => e.Text).ToArray()));
        }
        
        ConvertedResult Convert(UnaryExpression unary)
        {
            switch (unary.NodeType)
            {
                case ExpressionType.Not:
                    return new ConvertedResult(typeof(bool), Convert(unary.Operand).Text.ConcatAround("NOT (", ")"));
                case ExpressionType.Convert:
                    var ret = Convert(unary.Operand);
                    var param = ret.Text as ParameterParts;
                    if (param != null && param.Value != null && !SupportedTypeSpec.IsSupported(param.Value.GetType()))
                    {
                        var casted = ExpressionToObject.ConvertObject(unary.Type, param.Value);
                        return new ConvertedResult(ret.Type, new ParameterParts(param.Name, param.MetaId, new DbParam() { Value = casted }));
                    }
                    return ret;
                case ExpressionType.ArrayLength:
                    object obj;
                    ExpressionToObject.GetExpressionObject(unary.Operand, out obj);
                    return new ConvertedResult(typeof(int), new ParameterParts(((Array)obj).Length));
                default:
                    return Convert(unary.Operand);
            }
        }

        ConvertedResult Convert(BinaryExpression binary)
        {
            if (binary.NodeType == ExpressionType.ArrayIndex)
            {
                object ary;
                ExpressionToObject.GetExpressionObject(binary.Left, out ary);
                object index;
                ExpressionToObject.GetExpressionObject(binary.Right, out index);
                return new ConvertedResult(typeof(int), new ParameterParts(((Array)ary).GetValue((int)index)));
            }

            var left = Convert(binary.Left);
            var right = Convert(binary.Right);

            if (left.Text.IsEmpty && right.Text.IsEmpty) return new ConvertedResult(null, string.Empty);
            if (left.Text.IsEmpty) return right;
            if (right.Text.IsEmpty) return left;

            //for null
            var nullCheck = ResolveNullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

            var nodeType = Convert(left, binary.NodeType, right);
            return new ConvertedResult(nodeType.Type, new HParts(left.Text.ConcatAround("(", ")"), nodeType.Text.ConcatAround(" ", " "), right.Text.ConcatAround("(", ")")));
        }
        
        ConvertedResult Convert(MemberExpression member)
        {
            //sub.Body
            var body = ResolveSqlExpressionBody(member);
            if (body != null) return body;

            //sql symbol.
            var symbolMember = member.GetMemberConverter();
            if (symbolMember != null)
            {
                //convert.
                return new ConvertedResult(member.Type, symbolMember.Convert(member, this));
            }

            //sql symbol extension method
            var method = member.Expression as MethodCallExpression;
            if (method != null)
            {
                var symbolMethod = method.GetMethodConverter();
                if (symbolMethod != null)
                {
                    var ret = symbolMethod.Convert(method, this);
                    //T()
                    var tbl = ret as DbTableParts;
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

        ConvertedResult Convert(MethodCallExpression method)
        {
            //convert symbol.
            var parts = GetMethodChains(method).Select(c=> c.GetMethodConverter().Convert(c, this)).ToArray();
            if (parts.Length == 0) return ResolveExpressionObject(method);

            //TODO ちょっと嫌すぎる。括弧を付けない方法を何か確立せねば
            if (parts.Length == 1 && typeof(DisableBracketsParts).IsAssignableFrom(parts[0].GetType()))
            {
                return new ConvertedResult(method.Method.ReturnType, parts[0]);
            }

            var core = new VParts(parts);

            return (typeof(SelectClauseParts).IsAssignableFrom(parts[0].GetType())) ?
                 new ConvertedResult(method.Method.ReturnType, new SelectQueryParts(core)) :
                 new ConvertedResult(method.Method.ReturnType, new QueryParts(core));
        }

        ConvertedResult ResolveSqlExpressionBody(MemberExpression member)
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
                if (!typeof(IMethodChain).IsAssignableFrom(method.Type) ||
                     member.Member.Name != "Body") return null;
                return Convert(method);
            }

            if (members.Count < 2) return null;
            members.Reverse();
            
            //check SqlExpression's Body
            if (!typeof(ISql).IsAssignableFrom(members[0].Type) ||
                members[1].Member.Name != "Body") return null;

            //for example, sub.Body
            if (members.Count == 2) return ResolveExpressionObject(members[0]);

            //for example, sub.Body.column.
            else return new ConvertedResult(member.Type, string.Join(".", members.Where((e, i) => i != 1).Select(e => e.Member.Name).ToArray()));
        }

        ConvertedResult Convert(ConvertedResult left, ExpressionType nodeType, ConvertedResult right)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal: return new ConvertedResult(typeof(bool), "=");
                case ExpressionType.NotEqual: return new ConvertedResult(typeof(bool), "<>");
                case ExpressionType.LessThan: return new ConvertedResult(typeof(bool), "<");
                case ExpressionType.LessThanOrEqual: return new ConvertedResult(typeof(bool), "<=");
                case ExpressionType.GreaterThan: return new ConvertedResult(typeof(bool), ">");
                case ExpressionType.GreaterThanOrEqual: return new ConvertedResult(typeof(bool), ">=");
                case ExpressionType.Add:
                    {
                        if (left.Type == typeof(string) || right.Type == typeof(string))
                        {
                            return new ConvertedResult(left.Type, new StringAddOperatorParts());
                        }
                        return new ConvertedResult(left.Type, "+");
                    }
                case ExpressionType.Subtract: return new ConvertedResult(left.Type, "-");
                case ExpressionType.Multiply: return new ConvertedResult(left.Type, "*");
                case ExpressionType.Divide: return new ConvertedResult(left.Type, "/");
                case ExpressionType.Modulo: return new ConvertedResult(left.Type, "%");
                case ExpressionType.And: return new ConvertedResult(typeof(bool), "AND");
                case ExpressionType.AndAlso: return new ConvertedResult(typeof(bool), "AND");
                case ExpressionType.Or: return new ConvertedResult(typeof(bool), "OR");
                case ExpressionType.OrElse: return new ConvertedResult(typeof(bool), "OR");
            }
            throw new NotImplementedException();
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

        ConvertedResult ResolveLambdicElement(string name)
        {
            TableInfo table;
            if (DbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new ConvertedResult(null, new DbTableParts(table));
            }
            ColumnInfo col;
            if (DbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new ConvertedResult(col.Type, new DbColumnParts(col));
            }
            return new ConvertedResult(null, name);
        }

        ConvertedResult ResolveNullCheck(ConvertedResult left, ExpressionType nodeType, ConvertedResult right)
        {
            string ope;
            switch (nodeType)
            {
                case ExpressionType.Equal: ope = " IS NULL"; break;
                case ExpressionType.NotEqual: ope = " IS NOT NULL"; break;
                default: return null;
            }

            var leftParam = left.Text as ParameterParts;
            var rightParam = right.Text as ParameterParts;

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
                    return new ConvertedResult(null, targetTexts[i].ConcatAround("(", ")" + ope));
                }
            }
            return null;
        }

        ConvertedResult ResolveExpressionObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj))
            {
                throw new NotSupportedException();
            }

            //array.
            if (obj != null && obj.GetType().IsArray)
            {
                var list = new List<Parts>();
                foreach (var e in (IEnumerable)obj)
                {
                    list.Add(Convert(e));
                }
                return new ConvertedResult(exp.Type, Arguments(list.ToArray()));
            }

            //object symbol.
            //for example enum.
            var symbol = exp.Type.GetObjectConverter();
            if (symbol != null) return new ConvertedResult(exp.Type, symbol.Convert(obj));

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
                return new ConvertedResult(exp.Type.GetGenericArguments()[0], new ParameterParts(name, metaId, param));
            }

            //ISqlExpression.
            //example [ from(exp) ]
            var sqlExp = obj as ISql;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new ConvertedResult(type, sqlExp.Parts);
            }

            //others.
            //Even if it is not a supported type, if it is correctly written it will be cast to the caller.
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
                return new ConvertedResult(exp.Type, new ParameterParts(name, metaId, new DbParam() { Value = obj }));
            }
        }
        
        static IEnumerable<MethodCallExpression> GetMethodChains(MethodCallExpression end)
        {
            var chains = new List<MethodCallExpression>();
            var curent = end;
            while (curent != null && curent.GetMethodConverter() != null)
            {
                chains.Insert(0, curent);
                curent = (curent.Method.IsExtension() && 0 < curent.Arguments.Count) ? 
                            curent.Arguments[0] as MethodCallExpression :
                            null;
            }
            return chains;
        }
    }
}
