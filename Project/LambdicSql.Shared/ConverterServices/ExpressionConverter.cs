using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LambdicSql.ConverterServices.Inside.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Helper to convert expression to text. 
    /// </summary>
    public class ExpressionConverter
    {
        DbInfo DbInfo { get; }

        ExpressionConverter(DbInfo info)
        {
            DbInfo = info;
        }

        internal static ICode CreateCode(DbInfo dbInfo, Expression core)
        {
            var converter = new ExpressionConverter(dbInfo);
            return core == null ? string.Empty.ToCode() : converter.ConvertToCode(core);
        }

        internal static ICode AddCode(ICode lhs, ICode rhs)
        {
            if (lhs.IsEmpty) return rhs;
            if (rhs.IsEmpty) return lhs;

            var selectQueryLeft = lhs as SelectQueryCode;
            var selectQueryRight = rhs as SelectQueryCode;

            if (selectQueryLeft == null && selectQueryRight == null) return new VCode(lhs, rhs);
            if (selectQueryLeft == null && selectQueryRight != null) return new VCode(lhs, selectQueryRight.Core);
            if (selectQueryLeft != null && selectQueryRight == null) return new SelectQueryCode(new VCode(selectQueryLeft.Core, rhs));
            return new SelectQueryCode(new VCode(selectQueryLeft.Core, selectQueryRight.Core));
        }

        /// <summary>
        /// Convert expression to object.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>object.</returns>
        public object ConvertToObject(Expression expression)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(expression, out obj)) throw new NotSupportedException();
            return obj;
        }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="obj">object.</param>
        /// <returns>text.</returns>
        public ICode ConvertToCode(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return Convert(exp);

            var param = obj as IDbParam;
            if (param != null) return new ParameterCode(null, null, param);

            return new ParameterCode(obj);
        }

        ICode Convert(Expression exp)
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

        ICode Convert(MemberInitExpression memberInit)
        {
            var value = ExpressionToObject.GetMemberInitObject(memberInit);
            return ConvertToCode(value);
        }

        ICode Convert(ConstantExpression constant)
        {
            //sql symbol.
            var symbol = constant.Type.GetObjectConverter();
            if (symbol != null) return symbol.Convert(constant.Value);

            //normal object.
            return ConvertToCode(constant.Value);
        }

        ICode Convert(NewExpression newExp)
        {
            //symbol.
            var symbol = newExp.GetNewConverter();
            if (symbol != null) return symbol.Convert(newExp, this);

            //object.
            var value = ExpressionToObject.GetNewObject(newExp);
            return ConvertToCode(value);
        }

        ICode Convert(NewArrayExpression array)
        {
            if (!SupportedTypeSpec.IsSupported(array.Type))
            {
                throw new NotSupportedException();
            }
            var obj = SupportedTypeSpec.ConvertArray(array.Type, array.Expressions.Select(e => ConvertToObject(e)));
            return new ParameterCode(obj);
        }

        ICode Convert(UnaryExpression unary)
        {
            switch (unary.NodeType)
            {
                case ExpressionType.Not:
                    return new AroundCode(Convert(unary.Operand), "NOT (", ")");

                case ExpressionType.Convert:
                    var ret = Convert(unary.Operand);
                    var param = ret as ParameterCode;
                    if (param != null && param.Value != null && !SupportedTypeSpec.IsSupported(param.Value.GetType()))
                    {
                        var casted = ExpressionToObject.ConvertObject(unary.Type, param.Value);
                        return new ParameterCode(param.Name, param.MetaId, new DbParamValueOnly() { Value = casted });
                    }
                    return ret;

                case ExpressionType.ArrayLength:
                    object obj;
                    ExpressionToObject.GetExpressionObject(unary.Operand, out obj);
                    return new ParameterCode(((Array)obj).Length);

                default:
                    return Convert(unary.Operand);
            }
        }

        ICode Convert(BinaryExpression binary)
        {
            if (binary.NodeType == ExpressionType.ArrayIndex)
            {
                object ary;
                ExpressionToObject.GetExpressionObject(binary.Left, out ary);
                object index;
                ExpressionToObject.GetExpressionObject(binary.Right, out index);
                return new ParameterCode(((Array)ary).GetValue((int)index));
            }

            var left = Convert(binary.Left);
            var right = Convert(binary.Right);

            //sql + sql
            //sql + clause
            if (binary.NodeType == ExpressionType.Add)
            {
                if (typeof(SqlExpression).IsClassAndAssignableFromEx(binary.Type) ||
                    typeof(SqlExpression).IsClassAndAssignableFromEx(binary.Left.Type) ||
                    typeof(SqlExpression).IsClassAndAssignableFromEx(binary.Right.Type))
                {
                    return AddCode(left, right);
                }
            }
            if (left.IsEmpty && right.IsEmpty) return string.Empty.ToCode();
            if (left.IsEmpty) return right;
            if (right.IsEmpty) return left;

            //for null
            var nullCheck = TryResolveNullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

            var nodeType = Convert(binary.Type, left, binary.NodeType, right);
            return new HCode(AddBinaryExpressionBlankets(left), nodeType, AddBinaryExpressionBlankets(right));
        }

        ICode Convert(MemberExpression member)
        {
            //sub.Body
            var body = TryResolveSqlExpressionBody(member);
            if (body != null) return body;

            //sql symbol.
            var symbolMember = member.GetMemberConverter();
            if (symbolMember != null)
            {
                //convert.
                return symbolMember.Convert(member, this);
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
                    var tbl = ret as DbTableCode;
                    if (tbl != null)
                    {
                        var memberName = tbl.Info.LambdaFullName + "." + member.Member.Name;
                        return ResolveLambdicElement(memberName);
                    }
                    throw new NotSupportedException();
                }
            }

            //db element.
            ICode code;
            if (TryGetDbDesignParamCode(member, out code))
            {
                return code;
            }

            //get value.
            return ResolveExpressionObject(member);
        }

        ICode Convert(MethodCallExpression method)
        {
            var chain = GetMethodChains(method);
            if (chain.Count == 0) return ResolveExpressionObject(method);

            //convert symbol.
            var code = new ICode[chain.Count];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chain[i].GetMethodConverter().Convert(chain[i], this);
            }
            
            //for ALL function. can't add blankets.
            if (code.Length == 1 && typeof(IDisableBinaryExpressionBrackets).IsAssignableFromEx(code[0].GetType()))
            {
                return code[0];
            }

            var core = new VCode(code);

            return (typeof(SelectClauseCode).IsAssignableFromEx(code[0].GetType())) ?
                 (ICode)new SelectQueryCode(core) :
                 new QueryCode(core);
        }
        
        ICode Convert(Type type, ICode left, ExpressionType nodeType, ICode right)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal: return OperatorCode.Equal;
                case ExpressionType.NotEqual: return OperatorCode.NotEqual;
                case ExpressionType.LessThan: return OperatorCode.LessThan;
                case ExpressionType.LessThanOrEqual: return OperatorCode.LessThanOrEqual;
                case ExpressionType.GreaterThan: return OperatorCode.GreaterThan;
                case ExpressionType.GreaterThanOrEqual: return OperatorCode.GreaterThanOrEqual;
                case ExpressionType.Add: return type == typeof(string) ? OperatorCode.AddString : OperatorCode.Add;
                case ExpressionType.Subtract: return OperatorCode.Subtract;
                case ExpressionType.Multiply: return OperatorCode.Multiply;
                case ExpressionType.Divide: return OperatorCode.Divide;
                case ExpressionType.Modulo: return OperatorCode.Modulo;
                case ExpressionType.And: return OperatorCode.And;
                case ExpressionType.AndAlso: return OperatorCode.AndAlso;
                case ExpressionType.Or: return OperatorCode.Or;
                case ExpressionType.OrElse: return OperatorCode.OrElse;
            }
            throw new NotImplementedException();
        }

        ICode AddBinaryExpressionBlankets(ICode src)
            => typeof(IDisableBinaryExpressionBrackets).IsAssignableFromEx(src.GetType()) ? src : new AroundCode(src, "(", ")");

        ICode TryResolveSqlExpressionBody(MemberExpression member)
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
            
            if (members.Count < 2) return null;

            //check SqlExpression's Body
            if (members[members.Count - 2].Member.Name != "Body") return null;
            if (!typeof(Sql).IsAssignableFromEx(members[members.Count - 1].Type)) return null;

            members.Reverse();

            //for example, sub.Body
            if (members.Count == 2) return ResolveExpressionObject(members[0]);

            //for example, sub.Body.column.
            else return string.Join(".", members.Where((e, i) => i != 1).Select(e => e.Member.Name).ToArray()).ToCode();
        }

        bool TryGetDbDesignParamCode(MemberExpression exp, out ICode code)
        {
            var member = exp;
            code = null;
            var names = new List<string>();
            while (member != null)
            {
                names.Insert(0, member.Member.Name);
                var table = member.GetMemberTableConverter();
                if (table != null)
                {
                    if (!string.IsNullOrEmpty(table.Name))
                    {
                        names[0] = table.Name;
                    }
                    var lambdaName = string.Join(".", names.ToArray());
                    if (names.Count == 1)
                    {
                        code = new DbTableCode(new TableInfo(lambdaName, lambdaName));
                    }
                    else
                    {
                        code = new DbColumnCode(new ColumnInfo(exp.Type, lambdaName, lambdaName, names.Last()));
                    }
                    return true;
                }
                if (member.Expression is ParameterExpression)
                {
                    //using ParameterExpression with LambdicSql only when it represents a component of db.
                    //for example, Sql<DB>.Create(db =>
                    var lambdaName = string.Join(".", names.ToArray());
                    code = ResolveLambdicElement(lambdaName);
                    return true;
                }
                member = member.Expression as MemberExpression;
            }
            return false;
        }

        ICode ResolveLambdicElement(string name)
        {
            string schema;
            if (DbInfo.TryGetSchema(name, out schema))
            {
                return new DbSchemaCode(schema);
            }
            TableInfo table;
            if (DbInfo.TryGetTable(name, out table))
            {
                return new DbTableCode(table);
            }
            ColumnInfo col;
            if (DbInfo.TryGetColumn(name, out col))
            {
                return new DbColumnCode(col);
            }
            return name.ToCode();
        }

        ICode TryResolveNullCheck(ICode left, ExpressionType nodeType, ICode right)
        {
            string ope;
            switch (nodeType)
            {
                case ExpressionType.Equal: ope = " IS NULL"; break;
                case ExpressionType.NotEqual: ope = " IS NOT NULL"; break;
                default: return null;
            }

            var leftParam = left as ParameterCode;
            var rightParam = right as ParameterCode;

            var leftObj = leftParam != null ? leftParam.Value : null;
            var rightObj = rightParam != null ? rightParam.Value : null;
            var bothParam = (leftParam != null && rightParam != null);

            var isParams = new[] { leftParam != null, rightParam != null };
            var objs = new[] { leftObj, rightObj };
            var names = new[] { left, right };
            var targetTexts = new[] { right, left };
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
                    return new AroundCode(targetTexts[i], "(", ")" + ope);
                }
            }
            return null;
        }

        ICode ResolveExpressionObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj))
            {
                throw new NotSupportedException();
            }

            //object symbol.
            //for example enum.
            var symbol = exp.Type.GetObjectConverter();
            if (symbol != null) return symbol.Convert(obj);

            //IDbParam.
            if (typeof(IDbParam).IsAssignableFromEx(exp.Type))
            {
                string name = string.Empty;
                MetaId metaId = null;
                var member = exp as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                    metaId = new MetaId(member.Member);
                }
                var param = obj as IDbParam;
                //use field name.
                return new ParameterCode(name, metaId, param);
            }

            //sql.
            //example [ IN(exp) ]
            var sqlExp = obj as Sql;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArgumentsEx();
                if (0 < types.Length) type = types[0];
                return sqlExp.Code;
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
                return new ParameterCode(name, metaId, new DbParamValueOnly() { Value = obj });
            }
        }
        
        static List<MethodCallExpression> GetMethodChains(MethodCallExpression end)
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
