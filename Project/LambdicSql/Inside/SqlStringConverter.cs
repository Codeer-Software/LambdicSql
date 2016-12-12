using LambdicSql.SqlBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        class DecodedInfo
        {
            internal Type Type { get; }
            internal IText Text { get; }
            internal DecodedInfo(Type type, IText text)
            {
                Type = type;
                Text = text;
            }
        }

        SqlConvertOption _option;
        ISqlSyntaxCustomizer _sqlSyntaxCustomizer;

        public bool UsingColumnNameOnly { get; set; }

        public SqlConvertingContext Context { get; }

        internal SqlStringConverter(SqlConvertingContext context, SqlConvertOption option, ISqlSyntaxCustomizer sqlSyntaxCustomizer)
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

        //TODO ここに再帰カウンターを付けることによって、それが0でなければサブクエリだってことが分かる！つけすぎるとうざいから、メソッド呼び出しのところで。
        public IText ToString(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return ToString(exp).Text;

            var param = obj as DbParam;
            if (param != null) return new SingleText(Context.Parameters.Push(param.Value, null, null, param));

            return new SingleText(Context.Parameters.Push(obj));
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
            var value = ExpressionToObject.GetMemberInitObject(memberInit);
            return new DecodedInfo(memberInit.Type, ToString(value));
        }

        DecodedInfo ToString(ConstantExpression constant)
        {
            //sql syntax.
            if (constant.Type.IsSqlSyntax()) return new DecodedInfo(constant.Type, new SingleText(constant.Value.ToString().ToUpper()));
            //normal object.
            if (SupportedTypeSpec.IsSupported(constant.Type)) return new DecodedInfo(constant.Type, ToString(constant.Value));

            throw new NotSupportedException();
        }

        DecodedInfo ToString(NewExpression newExp)
        {
            //syntax.
            if (newExp.Type.IsSqlSyntax())
            {
                //exist customizer.
                if (_sqlSyntaxCustomizer != null)
                {
                    var ret = _sqlSyntaxCustomizer.ToString(this, newExp);
                    if (ret != null) return new DecodedInfo(null, ret);
                }

                //convert expression to text.
                var func = newExp.GetConverotrMethod();
                return new DecodedInfo(null, func(this, newExp));
            }

            //object.
            var value = ExpressionToObject.GetNewObject(newExp);
            return new DecodedInfo(newExp.Type, ToString(value));
        }

        DecodedInfo ToString(NewArrayExpression array)
        {
            if (array.Expressions.Count == 0) return new DecodedInfo(null, new SingleText(string.Empty));
            var infos = array.Expressions.Select(e => ToString(e)).ToArray();
            return new DecodedInfo(infos[0].Type, new HorizontalText(", ", infos.Select(e=>e.Text).ToArray()));
        }

        DecodedInfo ToString(UnaryExpression unary)
        {
            switch (unary.NodeType)
            {
                case ExpressionType.Not:
                    return new DecodedInfo(typeof(bool), ToString(unary.Operand).Text.ConcatAround("NOT (", ")"));
                case ExpressionType.Convert:
                    var ret = ToString(unary.Operand);

                    //In the case of parameters, execute casting.
                    //TODO これだめだ。内部的に何度もパラメータ変換が実行される ToString(0)
                    var paramText = ret.Text.ToString(0);
                    object obj;
                    if (Context.Parameters.TryGetParam(paramText, out obj))
                    {
                        if (obj != null && !SupportedTypeSpec.IsSupported(obj.GetType()))
                        {
                            Context.Parameters.ChangeObject(paramText, ExpressionToObject.ConvertObject(unary.Type, obj));
                        }
                    }
                    return ret;
                //TODO
                case ExpressionType.ArrayLength:
                    throw new NotSupportedException();
                case ExpressionType.ArrayIndex:
                    throw new NotSupportedException();
                default:
                    return ToString(unary.Operand);
            }
        }

        DecodedInfo ToString(BinaryExpression binary)
        {
            var left = ToString(binary.Left);
            var right = ToString(binary.Right);

            if (left.Text.IsEmpty && right.Text.IsEmpty) return new DecodedInfo(null, new SingleText(string.Empty));
            if (left.Text.IsEmpty) return right;
            if (right.Text.IsEmpty) return left;

            //for null
            var nullCheck = ResolveNullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

            var nodeType = ToString(left, binary.NodeType, right);
            return new DecodedInfo(nodeType.Type, new HorizontalText(left.Text.ConcatAround("(", ")"), nodeType.Text.ConcatAround(" ", " "), right.Text.ConcatAround("(", ")")));
        }
        
        DecodedInfo ToString(MemberExpression member)
        {
            //sub.Body
            var body = ResolveSqlExpressionBody(member);
            if (body != null) return body;

            //sql syntax.
            if (member.Member.DeclaringType.IsSqlSyntax())
            {
                //exist customizer.
                if (_sqlSyntaxCustomizer != null)
                {
                    var custom = _sqlSyntaxCustomizer.ToString(this, member);
                    if (custom != null)
                    {
                        return new DecodedInfo(member.Type, custom);
                    }
                }
                //convert.
                return new DecodedInfo(member.Type, member.GetConverotrMethod()(this, member));
            }

            //sql syntax extension method
            var method = member.Expression as MethodCallExpression;
            if (method != null && method.Method.DeclaringType.IsSqlSyntax())
            {
                var memberName = method.GetConverotrMethod()(this, new[] { method }) + "." + member.Member.Name;

                TableInfo table;
                if (Context.DbInfo.GetLambdaNameAndTable().TryGetValue(memberName, out table))
                {
                    return new DecodedInfo(null, new SingleText(table.SqlFullName));
                }
                string col;
                Type colType;
                if (TryGetValueLambdaNameAndColumn(memberName, out col, out colType))
                {
                    return new DecodedInfo(colType, new SingleText(col));
                }
                return new DecodedInfo(null, new SingleText(memberName));
            }

            //db element.
            string name;
            if (IsDbDesignParam(member, out name))
            {
                TableInfo table;
                if (Context.DbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
                {
                    return new DecodedInfo(null, new SingleText(table.SqlFullName));
                }
                string col;
                Type colType;
                if (TryGetValueLambdaNameAndColumn(name, out col, out colType))
                {
                    return new DecodedInfo(colType, new SingleText(col));
                }
                throw new NotSupportedException();
            }

            //get value.
            return ResolveExpressionObject(member);
        }

        public bool TryGetValueLambdaNameAndColumn(string lambdaName, out string columnName, out Type colType)
        {
            columnName = string.Empty;
            colType = null;
            ColumnInfo col;
            if (!Context.DbInfo.GetLambdaNameAndColumn().TryGetValue(lambdaName, out col)) return false;

            colType = col.Type;
            if (UsingColumnNameOnly) columnName = col.SqlColumnName;
            else columnName = col.SqlFullName;
            return true;
        }

        DecodedInfo ToString(MethodCallExpression method)
        {
            //not sql syntax.
            if (!method.Method.DeclaringType.IsSqlSyntax()) return ResolveExpressionObject(method);

            var ret = new List<IText>();
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

                //convert.
                ret.Add(chain[0].GetConverotrMethod()(this, chain));
            }

            return new DecodedInfo(method.Method.ReturnType, new VerticalText(ret.ToArray()));
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
            else return new DecodedInfo(member.Type, new SingleText(string.Join(".", members.Where((e, i) => i != 1).Select(e => e.Member.Name).ToArray())));
        }

        DecodedInfo ToString(DecodedInfo left, ExpressionType nodeType, DecodedInfo right)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal: return new DecodedInfo(typeof(bool), new SingleText("="));
                case ExpressionType.NotEqual: return new DecodedInfo(typeof(bool), new SingleText("<>"));
                case ExpressionType.LessThan: return new DecodedInfo(typeof(bool), new SingleText("<"));
                case ExpressionType.LessThanOrEqual: return new DecodedInfo(typeof(bool), new SingleText("<="));
                case ExpressionType.GreaterThan: return new DecodedInfo(typeof(bool), new SingleText(">"));
                case ExpressionType.GreaterThanOrEqual: return new DecodedInfo(typeof(bool), new SingleText(">="));
                case ExpressionType.Add:
                    {
                        if (left.Type == typeof(string) || right.Type == typeof(string))
                        {
                            return new DecodedInfo(left.Type, new SingleText(_option.StringAddOperator));
                        }
                        return new DecodedInfo(left.Type, new SingleText("+"));
                    }
                case ExpressionType.Subtract: return new DecodedInfo(left.Type, new SingleText("-"));
                case ExpressionType.Multiply: return new DecodedInfo(left.Type, new SingleText("*"));
                case ExpressionType.Divide: return new DecodedInfo(left.Type, new SingleText("/"));
                case ExpressionType.Modulo: return new DecodedInfo(left.Type, new SingleText("%"));
                case ExpressionType.And: return new DecodedInfo(typeof(bool), new SingleText("AND"));
                case ExpressionType.AndAlso: return new DecodedInfo(typeof(bool), new SingleText("AND"));
                case ExpressionType.Or: return new DecodedInfo(typeof(bool), new SingleText("OR"));
                case ExpressionType.OrElse: return new DecodedInfo(typeof(bool), new SingleText("OR"));
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
            //TODO .ToString(0)多い
            //TODO これだめだ。内部的に何度もパラメータ変換が実行される ToString(0)
            object leftObj, rightObj;
            var leftIsParam = Context.Parameters.TryGetParam(left.Text.ToString(0), out leftObj);
            var rightIsParam = Context.Parameters.TryGetParam(right.Text.ToString(0), out rightObj);
            var bothParam = (leftIsParam && rightIsParam);

            var isParams = new[] { leftIsParam, rightIsParam };
            var objs = new[] { leftObj, rightObj };
            var names = new[] { left.Text.ToString(0), right.Text.ToString(0) };
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
                    Context.Parameters.Remove(names[i]);
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
                var list = new List<IText>();
                foreach (var e in (IEnumerable)obj)
                {
                    list.Add(ToString(e));
                }
                return new DecodedInfo(exp.Type, new HorizontalText(", ", list.ToArray()));
            }

            //value type is SqlSyntax
            //for example [ enum ]
            if (exp.Type.IsSqlSyntax())
            {
                if (_sqlSyntaxCustomizer != null)
                {
                    var ret = _sqlSyntaxCustomizer.ToString(this, obj);
                    if (ret != null) return new DecodedInfo(exp.Type, ret);
                }
                return new DecodedInfo(exp.Type, new SingleText(obj.ToString().ToUpper()));
            }

            //normal object.
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
                return new DecodedInfo(exp.Type, new SingleText(Context.Parameters.Push(obj, name, metadataToken)));
            }

            //DbParam.
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
                return new DecodedInfo(exp.Type.GetGenericArguments()[0], new SingleText(Context.Parameters.Push(obj, name, metadataToken, param)));
            }
            
            //SqlExpression.
            //example [ from(exp) ]
            var sqlExp = obj as ISqlExpressionBase;
            if (sqlExp != null)
            {
                Type type = null;
                var types = sqlExp.GetType().GetGenericArguments();
                if (0 < types.Length) type = types[0];
                return new DecodedInfo(type, SqlDisplayAdjuster.AdjustSubQueryString(sqlExp.ToString(this)));
            }

            //others.
            //If it is correctly written it will be cast at the caller.
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
                return new DecodedInfo(exp.Type, new SingleText(Context.Parameters.Push(obj, name, metadataToken)));
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
