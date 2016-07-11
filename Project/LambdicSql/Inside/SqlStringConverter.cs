using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    class SqlStringConverter : ISqlStringConverter
    {
        enum SpecialElementType
        {
            Table,
            Column,
            SubQuery
        }

        class SqlStringConvertingEventArgs : EventArgs
        {
            internal SpecialElementType SpecialElementType { get; set; }
            internal string Detail { get; set; }
            internal SqlStringConvertingEventArgs(SpecialElementType type, string detail)
            {
                SpecialElementType = type;
                Detail = detail;
            }
        }

        DbInfo _dbInfo;
        IQueryCustomizer _queryCustomizer;
        PrepareParameters _parameters;
        EventHandler<SqlStringConvertingEventArgs> SpecialElementConverting = (_, __)=> { };
        bool _isTopLevelQuery;

        public DbInfo DbInfo => _dbInfo;

        internal SqlStringConverter(DbInfo dbInfo, PrepareParameters parameters, IQueryCustomizer queryCustomizer, bool isTopLevelQuery)
        {
            _dbInfo = dbInfo;
            _parameters = parameters;
            _queryCustomizer = queryCustomizer;
            _isTopLevelQuery = isTopLevelQuery;
        }

        internal static bool IsSubQuery(MethodCallExpression method)
            => method.Arguments.Count == 1 &&
                    typeof(IQuery).IsAssignableFrom(method.Arguments[0].Type) &&
                    method.Method.DeclaringType == typeof(QueryExtensions) &&
                    method.Method.Name == nameof(QueryExtensions.Cast);

        internal static string ToString(IQuery query, PrepareParameters parameters, IQueryCustomizer queryCustomizer)
            => ToStringCore(query, parameters, queryCustomizer, true);

        static string ToStringCore(IQuery query, PrepareParameters parameters, IQueryCustomizer queryCustomizer, bool isTopLevelQuery)
           => new SqlStringConverter(query.Db, parameters, queryCustomizer, isTopLevelQuery).ToString(query);
        
        string ToString(IQuery query)
        {
            var clauses = query.GetClausesClone();
            if (_queryCustomizer != null)
            {
                clauses = _queryCustomizer.CustomClauses(clauses);
            }
            var convertor = new SqlStringConverter(_dbInfo, _parameters, _queryCustomizer, false);
            var text = string.Join(Environment.NewLine, clauses.Select(e => e.ToString(this)).ToArray());
            if (_isTopLevelQuery)
            {
                return text + ";";
            }
            else
            {
                return "(" + string.Join(" ", text.Replace(Environment.NewLine, " ").Replace("\t", " ").
                       Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) + ")";
            }
        }

        public string ToString(object obj)
        {
            var exp = obj as Expression;
            if (exp != null)
            {
                return ToString(exp).Text;
            }

            var query = obj as IQuery;
            if (query != null)
            {
                return ToString(query);
            }
            return _parameters.Push(obj);
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

            throw new NotSupportedException();
        }

        DecodedInfo ToString(UnaryExpression unary)
            => unary.NodeType == ExpressionType.Not ?
                new DecodedInfo(typeof(bool), "NOT (" + ToString(unary.Operand) + ")") : ToString(unary.Operand);

        DecodedInfo ToString(MethodCallExpression method)
        {
            //sub query.
            if (IsSubQuery(method))
            {
                //notify converting info.
                SpecialElementConverting(this, new SqlStringConvertingEventArgs(SpecialElementType.SubQuery, string.Empty));

                var paramQueryCustomizer = Expression.Parameter(typeof(IQueryCustomizer), "queryCustomizer");
                var paramPrepare = Expression.Parameter(typeof(PrepareParameters), "prepareParameters");
                var call = Expression.Call(null, GetType().
                    GetMethod(nameof(MakeQueryString), BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public), new[] { method.Arguments[0], paramQueryCustomizer, paramPrepare });
                var funcSubQuery = Expression.Lambda(call, new[] { paramQueryCustomizer, paramPrepare }).Compile();
                return new DecodedInfo(funcSubQuery.Method.ReturnType, funcSubQuery.DynamicInvoke(_queryCustomizer, _parameters).ToString());
            }

            //word
            if (0 < method.Arguments.Count && typeof(ISqlWord).IsAssignableFrom(method.Arguments[0].Type))
            {
                return CusotmInvoke(method, CustomTargetType.Word);
            }

            //func
            if (0 < method.Arguments.Count && typeof(ISqlFunc).IsAssignableFrom(method.Arguments[0].Type))
            {
                return CusotmInvoke(method, CustomTargetType.Func);
            }

            //normal
            //check
            CheckNormalFuncArguments(method);

            //invoke
            var funcNormal = Expression.Lambda(method).Compile();
            return new DecodedInfo(funcNormal.Method.ReturnType, ToString(funcNormal.DynamicInvoke()));
        }

        DecodedInfo CusotmInvoke(MethodCallExpression method, CustomTargetType invokeType)
        {
            //sql function.
            var argumentsSrc = method.Arguments.Skip(1).ToArray();//skip this. 

            //custom by user.
            var arguments = argumentsSrc.Select(e => ToString(e)).ToArray();
            if (_queryCustomizer != null)
            {
                var customed = _queryCustomizer.CusotmInvoke(invokeType, method.Method.ReturnType, method.Method.Name, arguments);
                if (!string.IsNullOrEmpty(customed))
                {
                    return new DecodedInfo(method.Method.ReturnType, customed);
                }
            }

            //custom by defined class.
            var custom = method.Method.DeclaringType.GetMethod(nameof(_queryCustomizer.CusotmInvoke));
            if (custom != null)
            {
                var customed = (string)custom.Invoke(null, new object[] { method.Method.ReturnType, method.Method.Name, arguments });
                if (!string.IsNullOrEmpty(customed))
                {
                    return new DecodedInfo(method.Method.ReturnType, customed);
                }
            }

            //normal format.
            return new DecodedInfo(method.Method.ReturnType, method.Method.Name + "(" + string.Join(", ", arguments.Select(e => e.Text).ToArray()) + ")");
        }

        void CheckNormalFuncArguments(MethodCallExpression method)
        {
            EventHandler<SqlStringConvertingEventArgs> check = (s, e) =>
            {
                string message = string.Empty;
                switch (e.SpecialElementType)
                {
                    case SpecialElementType.Table:
                        message = string.Format("can't use table({0}) in {1}", e.Detail, method.Method.Name);
                        break;
                    case SpecialElementType.Column:
                        message = string.Format("can't use column({0}) in {1}", e.Detail, method.Method.Name);
                        break;
                    case SpecialElementType.SubQuery:
                        message = string.Format("can't use sub query in {0}", method.Method.Name);
                        break;
                }
                throw new InvalidDbItemException(message);
            };
            SpecialElementConverting += check;
            try
            {
                method.Arguments.ToList().ForEach(e =>
                {
                    try
                    {
                        ToString(e);
                    }
                    catch (InvalidDbItemException exception)
                    {
                        throw exception;
                    }
                    catch (Exception) { }//ignore other exception, because it will be checked at invokeing.
                });
            }
            finally
            {
                SpecialElementConverting -= check;
            }
        }

        static string MakeQueryString(IQuery query, IQueryCustomizer queryCustomizer, PrepareParameters parameters)
            => ToStringCore(query, parameters, queryCustomizer, false);

        DecodedInfo ToString(BinaryExpression binary)
        {
            var left = ToString(binary.Left);
            var right = ToString(binary.Right);
            var nodeType = ToString(left, binary.NodeType, right);

            //for null
            var nullCheck = NullCheck(left, binary.NodeType, right);
            if (nullCheck != null)
            {
                return nullCheck;
            }
            return new DecodedInfo(nodeType.Type, "(" + left.Text + ") " + nodeType.Text + " (" + right.Text + ")");
        }

        DecodedInfo NullCheck(DecodedInfo left, ExpressionType nodeType, DecodedInfo right)
        {
            string ope;
            switch (nodeType)
            {
                case ExpressionType.Equal: ope = " IS NULL"; break;
                case ExpressionType.NotEqual: ope = " IS NOT NULL"; break;
                default: return null;
            }

            object leftObj, rightObj;
            var leftIsParam = _parameters.TryGetParam(left.Text, out leftObj);
            var rightIsParam = _parameters.TryGetParam(right.Text, out rightObj);
            if (leftIsParam && rightIsParam)
            {
                return null;
            }
            var isParams = new[] { leftIsParam, rightIsParam };
            var objs = new[] { leftObj, rightObj};
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
                        return null;
                    }
                    _parameters.Remove(names[i]);
                    return new DecodedInfo(null, "(" + targetTexts[i] + ")" + ope);
                }
            }
            return null;
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

        DecodedInfo ToString(ConstantExpression constant)
        {
            var func = Expression.Lambda(constant).Compile();
            return new DecodedInfo(func.Method.ReturnType, ToString(func.DynamicInvoke()));
        }

        DecodedInfo ToString(MemberExpression member)
        {
            string name = GetMemberCheckName(member);

            TableInfo table;
            if (_dbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                //notify converting info.
                SpecialElementConverting(this, new SqlStringConvertingEventArgs(SpecialElementType.Table, name));
                return new DecodedInfo(null, table.SqlFullName);
            }
            ColumnInfo col;
            if (_dbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                //notify converting info.
                SpecialElementConverting(this, new SqlStringConvertingEventArgs(SpecialElementType.Column, name));
                return new DecodedInfo(col.Type, col.SqlFullName);
            }
            if (HasParameter(member))
            {
                return new DecodedInfo(null, name);
            }
            var func = Expression.Lambda(member).Compile();
            return new DecodedInfo(func.Method.ReturnType, ToString(func.DynamicInvoke()));
        }

        static bool HasParameter(MemberExpression member)
        {
            while (member != null)
            {
                if (member.Expression is ParameterExpression)
                {
                    return true;
                }
                member = member.Expression as MemberExpression;
            }
            return false;
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
    }
}
