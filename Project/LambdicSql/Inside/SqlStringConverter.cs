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
        PrepareParameters _prepare;
        int _nestLevel;

        EventHandler<SqlStringConvertingEventArgs> SpecialElementConverting = (_, __)=> { };

        public DbInfo DbInfo => _dbInfo;

        public string ResolvePrepare(string value)
        {
            object obj;
            if (!_prepare.TryGetParam(value, out obj))
            {
                return value;
            }
            _prepare.Remove(value);
            return obj.ToString();
        }

        internal string ContinueConditionExpressionText { get; set; }

        public string ToString(object obj)
        {
            var exp = obj as Expression;
            if (exp != null) return ToString(exp).Text;

         //   var query = obj as IQuery;
           // if (query != null) return ToString(query);

            return _prepare.Push(obj);
        }

        internal SqlStringConverter(DbInfo dbInfo, PrepareParameters parameters, IQueryCustomizer queryCustomizer, int nestLevel)
        {
            _dbInfo = dbInfo;
            _prepare = parameters;
            _queryCustomizer = queryCustomizer;
            _nestLevel = nestLevel;
        }

        internal static bool IsSubQuery(MethodCallExpression method)
            => false;

        internal static bool IsSqlExpression(MethodCallExpression method)
            => method.Arguments.Count == 1 &&
                    typeof(ISqlExpression).IsAssignableFrom(method.Arguments[0].Type) &&
                    method.Method.DeclaringType == typeof(SqlExpressionExtensions) &&
                    method.Method.Name == nameof(SqlExpressionExtensions.Cast);
        /*
        internal static string ToString(IQuery query, PrepareParameters parameters, IQueryCustomizer queryCustomizer)
            => ToStringCore(query, parameters, queryCustomizer, 0);

        static string ToStringCore(IQuery query, PrepareParameters parameters, IQueryCustomizer queryCustomizer, int nestLevel)
           => new SqlStringConverter(query.Db, parameters, queryCustomizer, nestLevel).ToString(query);

        string ToString(IQuery query)
        {
            var clauses = query.GetClausesClone();
            if (_queryCustomizer != null) clauses = _queryCustomizer.CustomClauses(clauses);
            var text = string.Join(Environment.NewLine, clauses.Select(e => e.ToString(this)).ToArray());

            if (_nestLevel == 0)
            {
                return string.Join(Environment.NewLine,
                        text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).
                        Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray()) + ";";
            }
            text = "(" + text + ")";
            return AddNestTab(text, _nestLevel);
        }*/

        static string AddNestTab(string text, int nestLevel)
        {
            var t = string.Empty;
            Enumerable.Range(0, nestLevel).ToList().ForEach(e => t += "\t");
            return Environment.NewLine + string.Join(Environment.NewLine,
                text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).
                Select(e => t + e).ToArray());
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
                new DecodedInfo(typeof(bool), "NOT (" + ToString(unary.Operand) + ")") : ToString(unary.Operand);

        DecodedInfo ToString(MethodCallExpression method)
        {
            /*
            //sub query.
            if (IsSubQuery(method))
            {
                //notify converting info.
                SpecialElementConverting(this, new SqlStringConvertingEventArgs(SpecialElementType.SubQuery, string.Empty));
                object obj;
                if (ExpressionToObject.GetMemberObject(method.Arguments[0] as MemberExpression, out obj))
                {
                    return new DecodedInfo(method.Method.ReturnType, MakeQueryString(obj as IQuery, _queryCustomizer, _prepare, _nestLevel));
                }
                else
                {
                    var paramQueryCustomizer = Expression.Parameter(typeof(IQueryCustomizer), "queryCustomizer");
                    var paramPrepare = Expression.Parameter(typeof(PrepareParameters), "prepareParameters");
                    var nestLevel = Expression.Parameter(typeof(int), "nestLevel");
                    var call = Expression.Call(null, GetType().
                        GetMethod(nameof(MakeQueryString), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), 
                                new[] { method.Arguments[0], paramQueryCustomizer, paramPrepare, nestLevel });
                    var funcSubQuery = Expression.Lambda(call, new[] { paramQueryCustomizer, paramPrepare, nestLevel }).Compile();
                    return new DecodedInfo(funcSubQuery.Method.ReturnType,
                        funcSubQuery.DynamicInvoke(_queryCustomizer, _prepare, _nestLevel).ToString());
                }
            }*/
            if (IsSqlExpression(method))
            {
                object obj;
                if (!ExpressionToObject.GetMemberObject(method.Arguments[0] as MemberExpression, out obj))
                {
                    throw new NotSupportedException();
                }
                var text = ((ISqlExpression)obj).ToString(this);
                //TODO
                if (text.Replace(Environment.NewLine, string.Empty).Trim().IndexOf("SELECT") == 0)
                {
                    text = "(" + AddNestTab(text, 1).TrimStart() + ")";
                }
                return new DecodedInfo(typeof(ISqlExpression), text);
            }

            //words
            if (0 < method.Arguments.Count && typeof(ISqlWords).IsAssignableFrom(method.Arguments[0].Type))
            {
                return CusotmInvoke(method);
            }

            //normal
            //check
            CheckNormalFuncArguments(method);

            //invoke
            var funcNormal = Expression.Lambda(method).Compile();
            return new DecodedInfo(funcNormal.Method.ReturnType, ToString(funcNormal.DynamicInvoke()));
        }

        List<MethodCallExpression>[] GetMethodChain(MethodCallExpression end)
        {
            var chainX = new List<List<MethodCallExpression>>();
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
                        chainX.Add(chain);
                        chainX.Reverse();
                        return chainX.ToArray();
                    }
                    curent = next;
                    if (next.Method.DeclaringType != type)
                    {
                        break;
                    }
                }
                chain.Reverse();
                chainX.Add(chain);
            }
        }

        DecodedInfo CusotmInvoke(MethodCallExpression method)
        {
            var chainX = GetMethodChain(method);
            var ret = new List<string>();
            foreach (var chain in chainX)
            {
                var custom = chain[0].Method.DeclaringType.GetMethod("MethodChainToString");
                if (custom != null)
                {
                    ret.Add((string)custom.Invoke(null, new object[] { this, chain.ToArray() }));
                }
                else
                {
                    ret.Add(CusotmInvokeOne(chain[0]).Text);
                }
            }

            //TODO
            var text = new DecodedInfo(method.Method.ReturnType, string.Join(string.Empty, ret.ToArray()));
            if (method.Method.Name == "Cast" && text.Text.Replace(Environment.NewLine, string.Empty).Trim().IndexOf("SELECT") == 0)
            {
               return new DecodedInfo(method.Method.ReturnType, "(" + AddNestTab(text.Text, 1).TrimStart() + ")");
            }
            return text;
        }
        
        DecodedInfo CusotmInvokeOne(MethodCallExpression method)
        {
            //sql function.
            var argumentsSrc = method.Arguments.Skip(1).ToArray();//skip this. 


            //custom by defined class.
            var custom = method.Method.DeclaringType.GetMethod("MethodToString");
            if (custom != null)
            {
                var customed = (string)custom.Invoke(null, new object[] { this, method });
                if (customed != null)
                {
                    return new DecodedInfo(method.Method.ReturnType, customed);
                }
            }

            //custom by user.
            var arguments = argumentsSrc.Select(e => ToString(e)).ToArray();
            if (_queryCustomizer != null)
            {
                var customed = _queryCustomizer.CusotmInvoke(method.Method.ReturnType, method.Method.Name, arguments);
                if (customed != null)
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

        /*
        static string MakeQueryString(IQuery query, IQueryCustomizer queryCustomizer, PrepareParameters parameters, int nestLevel)
            => ToStringCore(query, parameters, queryCustomizer, nestLevel + 1);
            */
        DecodedInfo ToString(BinaryExpression binary)
        {
            var left = ToString(binary.Left);
            var right = ToString(binary.Right);

            if (string.IsNullOrEmpty(left.Text) && string.IsNullOrEmpty(right.Text)) return new DecodedInfo(null, string.Empty);
            if (string.IsNullOrEmpty(left.Text)) return right;
            if (string.IsNullOrEmpty(right.Text)) return left;

            var nodeType = ToString(left, binary.NodeType, right);

            //for null
            var nullCheck = NullCheck(left, binary.NodeType, right);
            if (nullCheck != null) return nullCheck;

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
            var leftIsParam = _prepare.TryGetParam(left.Text, out leftObj);
            var rightIsParam = _prepare.TryGetParam(right.Text, out rightObj);
            if (leftIsParam && rightIsParam) return null;

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
                    if (!nullObj) return null;
                    _prepare.Remove(names[i]);
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
            if (constant.Value == null) return new DecodedInfo(null, ToString((object)null));

            var type = constant.Value.GetType();
            if (SupportedTypeSpec.IsSupported(type)) return new DecodedInfo(type, ToString(constant.Value));
            if (type.IsEnum) return new DecodedInfo(type, constant.Value.ToString());

            throw new NotSupportedException();
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

            //TODO naming rule.
            var method = member.Expression as MethodCallExpression;
            if (method != null && IsSqlExpression(method))
            {
                var mem2 = method.Arguments[0] as MemberExpression;
                return new DecodedInfo(null, mem2.Member.Name + "." + name);
            }

            if (HasParameter(member)) return new DecodedInfo(null, name);

            var decoded = MemberToStringByLambda(member);
            if (decoded != null) return decoded;

            throw new NotSupportedException();
        }

        DecodedInfo MemberToStringByLambda(MemberExpression member)
        {
            object obj;
            if (!ExpressionToObject.GetMemberObject(member, out obj)) return null;
            if (obj == null) return new DecodedInfo(null, ToString((object)null));
            if (SupportedTypeSpec.IsSupported(obj.GetType())) return new DecodedInfo(obj.GetType(), ToString(obj));
            return null;
        }

        static bool HasParameter(MemberExpression member)
        {
            while (member != null)
            {
                if (member.Expression is ParameterExpression) return true;
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
