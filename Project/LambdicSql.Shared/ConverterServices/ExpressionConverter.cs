using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Collections.Generic;
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
        static readonly Dictionary<ExpressionType, int> ExpressionTypeJoinPriority = new Dictionary<ExpressionType, int>
        {
            { ExpressionType.Or                  , 0},
            { ExpressionType.OrElse              , 0},
            { ExpressionType.And                 , 1},
            { ExpressionType.AndAlso             , 1},
            { ExpressionType.LessThan            , 2},
            { ExpressionType.LessThanOrEqual     , 2},
            { ExpressionType.GreaterThan         , 2},
            { ExpressionType.GreaterThanOrEqual  , 2},
            { ExpressionType.Equal               , 3},
            { ExpressionType.NotEqual            , 3},
            { ExpressionType.Add                 , 4},
            { ExpressionType.Subtract            , 4},
            { ExpressionType.Multiply            , 5},
            { ExpressionType.Divide              , 5},
            { ExpressionType.Modulo              , 5},
        };

        class AddingBlankets
        {
            internal bool Left { get; set; }
            internal bool Right { get; set; }
        }

        DbInfo DbInfo { get; }

        internal ExpressionConverter(DbInfo info)
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

            var topQueryLeft = lhs as ITopQueryCode;
            var topQueryRight = rhs as ITopQueryCode;

            if (topQueryLeft == null && topQueryRight == null) return new VCode(lhs, rhs);
            if (topQueryLeft == null && topQueryRight != null) return new VCode(lhs, topQueryRight.Core);
            if (topQueryLeft != null && topQueryRight == null) return topQueryLeft.Create(new VCode(topQueryLeft.Core, rhs));
            return topQueryLeft.Create(new VCode(topQueryLeft.Core, topQueryRight.Core));
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
            if (!SupportedTypeSpec.IsSupported(array.Type)) throw new NotSupportedException();

            var objs = new object[array.Expressions.Count];
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = ConvertToObject(array.Expressions[i]);
            }
            var obj = SupportedTypeSpec.ConvertArray(array.Type, objs);
            return new ParameterCode(obj);
        }

        ICode Convert(UnaryExpression unary)
        {
            switch (unary.NodeType)
            {
                case ExpressionType.Not:
                    return NotAddBrankets(Convert(unary.Operand));

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
            
            var isAddBlankets = CheckAddingBlanckets(binary, left, right);
            return new BinaryExpressionCode(new HCode(
                isAddBlankets.Left ? AddBinaryExpressionBlankets(left) : left,
                Convert(binary.Type, left, binary.NodeType, right),
                isAddBlankets.Right ? AddBinaryExpressionBlankets(right) : right));
        }

        ICode Convert(MemberExpression member)
        {
            //sub.Body or sub.Name
            var bodyOrName = TryResolveSqlExpressionBodyOrName(member);
            if (bodyOrName != null) return bodyOrName;

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
                    else
                    {
                        return new HCode(ret, new SingleTextCode("." + member.Member.Name));
                    }
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

            var core = new VCode(code);

            var topQuery = code[0] as ITopQueryCode;
            return topQuery != null ?
                 (ICode)topQuery.Create(core) :
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

        ICode TryResolveSqlExpressionBodyOrName(MemberExpression member)
        {
            //get all members.
            var members = new List<MemberExpression>();
            var exp = member;
            while (exp != null)
            {
                members.Add(exp);
                exp = exp.Expression as MemberExpression;
                if (exp != null) member = exp;
            }
            
            if (members.Count < 2) return null;

            members.Reverse();

            //check SqlExpression
            if (!typeof(Sql).IsAssignableFromEx(members[0].Type)) return null;

            //Name
            if (members[1].Member.Name == "Name")
            {
                if (members.Count != 2) return null;
                return members[0].Member.Name.ToCode();
            }

            //Body
            if (members[1].Member.Name != "Body") return null;

            //for example, sub.Body
            if (members.Count == 2) return ResolveExpressionObject(members[0]);

            //for example, sub.Body.column.
            else
            {
                var names = new string[members.Count - 1];
                names[0] = members[0].Member.Name;
                for (int i = 2; i < members.Count; i++)
                {
                    names[i - 1] = members[i].Member.Name;
                }
                return string.Join(".", names).ToCode();
            }
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
                    code = (names.Count == 1) ?
                         (ICode)new DbTableCode(new TableInfo(lambdaName, lambdaName)) :
                         new DbColumnCode(new ColumnInfo(exp.Type, lambdaName, lambdaName, names[names.Count - 1]));
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
            if (DbInfo.TryGetSchema(name, out schema)) return new DbSchemaCode(schema);

            TableInfo table;
            if (DbInfo.TryGetTable(name, out table)) return new DbTableCode(table);

            ColumnInfo col;
            if (DbInfo.TryGetColumn(name, out col)) return new DbColumnCode(col);

            return name.ToCode();
        }

        ICode TryResolveNullCheck(ICode left, ExpressionType nodeType, ICode right)
        {
            string ope;
            switch (nodeType)
            {
                case ExpressionType.Equal: ope = "IS NULL"; break;
                case ExpressionType.NotEqual: ope = "IS NOT NULL"; break;
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
                    return IsNullAddBrankets(targetTexts[i], ope);
                }
            }
            return null;
        }

        ICode ResolveExpressionObject(Expression exp)
        {
            object obj;
            if (!ExpressionToObject.GetExpressionObject(exp, out obj)) throw new NotSupportedException();

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
                return SqlCode.Create(sqlExp.Code);
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

        static AddingBlankets CheckAddingBlanckets(BinaryExpression binary, ICode left, ICode right)
        {
            var leftBinary = binary.Left as BinaryExpression;
            var rightBinary = binary.Right as BinaryExpression;
            return new AddingBlankets
            {
                Left = left is ISqlCode || (leftBinary != null && ExpressionTypeJoinPriority[leftBinary.NodeType] < ExpressionTypeJoinPriority[binary.NodeType]),
                Right = right is ISqlCode || (rightBinary != null && ExpressionTypeJoinPriority[rightBinary.NodeType] <= ExpressionTypeJoinPriority[binary.NodeType])
            };
        }

        static ICode AddBinaryExpressionBlankets(ICode src)
            => new AroundCode(src, "(", ")");

        static ICode NotAddBrankets(ICode target)
        {
            var addBlankets = target is ISqlCode || target is BinaryExpressionCode;
            return addBlankets ? (ICode)new AroundCode(target, "NOT (", ")") : new HCode("NOT".ToCode(), target) { Separator = " " };
        }

        static ICode IsNullAddBrankets(ICode target, string ope)
        {
            var addBlankets = target is ISqlCode || target is BinaryExpressionCode;
            return addBlankets ? (ICode)new AroundCode(target, "(", ") " + ope) : new HCode(target, ope.ToCode()) { Separator = " " };
        }
    }
}
