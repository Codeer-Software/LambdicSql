using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    class ExpressionDecoder : IExpressionDecoder
    {
        DbInfo _dbInfo;
        IQueryCustomizer _queryCustomizer;

        public DbInfo DbInfo => _dbInfo;

        internal ExpressionDecoder(DbInfo dbInfo, IQueryCustomizer queryCustomizer)
        {
            _dbInfo = dbInfo;
            _queryCustomizer = queryCustomizer;
        }

        internal static string ToString(IQuery query, IQueryCustomizer queryCustomizer)
            => ToStringCore(query, queryCustomizer) + ";";

        internal static string ToStringCore(IQuery query, IQueryCustomizer queryCustomizer)
           => new ExpressionDecoder(query.Db, queryCustomizer).ToString(query);
        
        internal string ToString(IQuery query)
        {
            var clauses = query.GetClausesClone();
            if (_queryCustomizer != null)
            {
                clauses = _queryCustomizer.CustomClauses(clauses);
            }
            return string.Join(Environment.NewLine, clauses.Select(e => e.ToString(this)).ToArray());
        }

        public string ToString(Expression exp) => ToStringCore(exp).Text;

        internal DecodedInfo ToStringCore(Expression exp)
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
                new DecodedInfo(typeof(bool), "NOT (" + ToStringCore(unary.Operand) + ")") : ToStringCore(unary.Operand);

        DecodedInfo ToString(MethodCallExpression method)
        {
            //sub query.
            if (0 < method.Arguments.Count && typeof(IQuery).IsAssignableFrom(method.Arguments[0].Type))
            {
                var param = Expression.Parameter(typeof(IQueryCustomizer), "queryCustomizer");
                var call = Expression.Call(null, GetType().
                    GetMethod("MakeQueryString", BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public), new[] { method.Arguments[0], param });
                var func = Expression.Lambda(call, new[] { param }).Compile();
                return new DecodedInfo(func.Method.ReturnType, func.DynamicInvoke(_queryCustomizer).ToString());
            }

            //normal func.
            if (method.Arguments.Count == 0 || !typeof(IDBFuncs).IsAssignableFrom(method.Arguments[0].Type))
            {
                var func = Expression.Lambda(method).Compile();
                return new DecodedInfo(func.Method.ReturnType, ToStringObject(func.DynamicInvoke()));
            }
                
            //db function.IDBFuncs
            var argumentsSrc = method.Arguments.Skip(1).ToArray();//skip this. 

            //custom
            var arguments = argumentsSrc.Select(e => ToStringCore(e)).ToArray();
            if (_queryCustomizer != null)
            {
                var customed = _queryCustomizer.CustomFunction(method.Method.ReturnType, method.Method.Name, arguments);
                if (!string.IsNullOrEmpty(customed))
                {
                    return new DecodedInfo(method.Method.ReturnType, customed);
                }
            }
            return new DecodedInfo(method.Method.ReturnType, method.Method.Name + "(" + string.Join(", ", arguments.Select(e=>e.Text).ToArray()) + ")");
        }

        static string MakeQueryString(IQuery query, IQueryCustomizer queryCustomizer)
            => "(" + string.Join(" ", ToStringCore(query, queryCustomizer).
                        Replace(Environment.NewLine, " ").Replace("\t", " ").
                        Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) + ")";

        DecodedInfo ToString(BinaryExpression binary)
        {
            var left = ToStringCore(binary.Left);
            var right = ToStringCore(binary.Right);
            var nodeType = ToString(left, binary.NodeType, right);
            return new DecodedInfo(nodeType.Type, "(" + left.Text + ") " + nodeType.Text + " (" + right.Text + ")");
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
            return new DecodedInfo(func.Method.ReturnType, ToStringObject(func.DynamicInvoke()));
        }

        DecodedInfo ToString(MemberExpression member)
        {
            //TODO I'll make best code.
            var name = string.Join(".", member.ToString().Split('.').Skip(1).ToArray());

            TableInfo table;
            if (_dbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new DecodedInfo(null, table.SqlFullName);
            }
            ColumnInfo col;
            if (_dbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new DecodedInfo(col.Type, col.SqlFullName);
            }
            var func = Expression.Lambda(member).Compile();
            return new DecodedInfo(func.Method.ReturnType, ToStringObject(func.DynamicInvoke().ToString()));
        }
        
        public string ToStringObject(object obj)
        {
            var exp = obj as Expression;
            if (exp != null)
            {
                return ToStringCore(exp).Text;
            }
            Type type = obj.GetType();
            if (type == typeof(string) || type == typeof(DateTime))
            {
                return "'" + obj + "'";
            }
            return obj.ToString();
        }

        public string MakeSqlArguments(IEnumerable<object> src)
        {
            var result = new List<string>();
            foreach (var arg in src)
            {
                var col = arg as ColumnInfo;
                result.Add(col == null ? ToStringObject(arg) : col.SqlFullName);
            }
            return string.Join(", ", result.ToArray());
        }
    }
}