using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    class ExpressionDecoder : IExpressionDecoder
    {
        QueryDecoder _queryParser;
        DbInfo _dbInfo;

        public DbInfo DbInfo => _dbInfo;

        internal ExpressionDecoder(DbInfo dbInfo, QueryDecoder queryParser)
        {
            _dbInfo = dbInfo;
            _queryParser = queryParser;
        }
        public string ToString(Expression exp) => ToStringCore(exp).Text;

        internal TypeAndText ToStringCore(Expression exp)
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

        TypeAndText ToString(UnaryExpression unary)
            => unary.NodeType == ExpressionType.Not ?
                new TypeAndText(typeof(bool), "NOT (" + ToStringCore(unary.Operand) + ")") : ToStringCore(unary.Operand);

        TypeAndText ToString(MethodCallExpression method)
        {
            //sub query.
            if (0 < method.Arguments.Count && typeof(IQuery).IsAssignableFrom(method.Arguments[0].Type))
            {
                var param = Expression.Parameter(typeof(QueryDecoder), "parser");
                var call = Expression.Call(null, GetType().
                    GetMethod("MakeQueryString", BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public), new[] { method.Arguments[0], param });
                var func = Expression.Lambda(call, new[] { param }).Compile();
                return new TypeAndText(func.Method.ReturnType, func.DynamicInvoke(_queryParser).ToString());
            }

            //db function.
            //TODO allow static function 
            var arguments = new List<string>();
            foreach (var arg in method.Arguments.Skip(1)) //skip this. 
            {
                arguments.Add(ToStringCore(arg).Text);
            }
            return new TypeAndText(method.Method.ReturnType, method.Method.Name + "(" + string.Join(", ", arguments.ToArray()) + ")");
        }

        static string MakeQueryString(IQuery query, QueryDecoder queryParser)
            => "(" + string.Join(" ", queryParser.ToStringCore((IQueryInfo)query).
                        Replace(Environment.NewLine, " ").Replace("\t", " ").
                        Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) + ")";

        TypeAndText ToString(BinaryExpression binary)
        {
            var left = ToStringCore(binary.Left);
            var right = ToStringCore(binary.Right);
            var nodeType = ToString(left, binary.NodeType, right);
            return new TypeAndText(nodeType.Type, "(" + left.Text + ") " + nodeType.Text + " (" + right.Text + ")");
        }

        TypeAndText ToString(TypeAndText left, ExpressionType nodeType, TypeAndText right)
        {
            Func<string, string> custom = @operator => _queryParser.CustomOperator(left.Type, @operator, right.Type);
            switch (nodeType)
            {
                case ExpressionType.Equal: return new TypeAndText(typeof(bool), custom("="));
                case ExpressionType.NotEqual: return new TypeAndText(typeof(bool), custom("<>"));
                case ExpressionType.LessThan: return new TypeAndText(typeof(bool), custom("<"));
                case ExpressionType.LessThanOrEqual: return new TypeAndText(typeof(bool), custom("<="));
                case ExpressionType.GreaterThan: return new TypeAndText(typeof(bool), custom(">"));
                case ExpressionType.GreaterThanOrEqual: return new TypeAndText(typeof(bool), custom(">="));
                case ExpressionType.Add: return new TypeAndText(left.Type, custom("+"));
                case ExpressionType.Subtract: return new TypeAndText(left.Type, custom("-"));
                case ExpressionType.Multiply: return new TypeAndText(left.Type, custom("*"));
                case ExpressionType.Divide: return new TypeAndText(left.Type, custom("/"));
                case ExpressionType.Modulo: return new TypeAndText(left.Type, custom("%"));
                case ExpressionType.And: return new TypeAndText(typeof(bool), custom("AND"));
                case ExpressionType.AndAlso: return new TypeAndText(typeof(bool), custom("AND"));
                case ExpressionType.Or: return new TypeAndText(typeof(bool), custom("OR"));
                case ExpressionType.OrElse: return new TypeAndText(typeof(bool), custom("OR"));
            }
            throw new NotImplementedException();
        }

        TypeAndText ToString(ConstantExpression constant)
        {
            var func = Expression.Lambda(constant).Compile();
            return new TypeAndText(func.Method.ReturnType, ToStringObject(func.DynamicInvoke()));
        }

        TypeAndText ToString(MemberExpression member)
        {
            //TODO I'll make best code.
            var name = string.Join(".", member.ToString().Split('.').Skip(1).ToArray());

            TableInfo table;
            if (_dbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return new TypeAndText(null, table.SqlFullName);
            }
            ColumnInfo col;
            if (_dbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return new TypeAndText(col.Type, col.SqlFullName);
            }
            var func = Expression.Lambda(member).Compile();
            return new TypeAndText(func.Method.ReturnType, ToStringObject(func.DynamicInvoke().ToString()));
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