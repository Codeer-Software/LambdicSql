using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    class ExpressionParser
    {
        QueryParser _queryParser;
        DbInfo _dbInfo;

        internal ExpressionParser(DbInfo dbInfo, QueryParser queryParser)
        {
            _dbInfo = dbInfo;
            _queryParser = queryParser;
        }

        internal static string GetElementName<TDB, T>(Expression<Func<TDB, T>> exp) where TDB : class
        {
            return GetElementName(exp.Body as MemberExpression);
        }

        internal string ToString(Expression exp)
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

        string ToString(UnaryExpression unary)
          => ToString(unary.Operand);

        string ToString(MethodCallExpression method)
        {
            //sub query.
            if (0 < method.Arguments.Count && typeof(IQuery).IsAssignableFrom(method.Arguments[0].Type))
            {
                var param = Expression.Parameter(typeof(QueryParser), "parser");
                var call = Expression.Call(null, GetType().
                    GetMethod("MakeQueryString", BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public), new[] { method.Arguments[0], param });
                var func = Expression.Lambda(call, new[] { param }).Compile();
                return func.DynamicInvoke(_queryParser).ToString();
            }

            //db function.
            var arguments = new List<string>();
            foreach (var arg in method.Arguments.Skip(1)) //skip this. TODO@ 両方使えるようにするか。IDBFuncsだったら。IDBFuncsを継承させる
            {
                arguments.Add(ToString(arg));
            }
            return method.Method.Name + "(" + string.Join(", ", arguments.ToArray()) + ")";
        }

        static string MakeQueryString(IQuery query, QueryParser queryParser)
            => "(" + string.Join(" ", queryParser.ToStringCore((IQueryInfo)query).
                        Replace(Environment.NewLine, " ").Replace("\t", " ").
                        Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) + ")";

        string ToString(BinaryExpression binary)
            => "(" + ToString(binary.Left) + ") " + ToString(binary.NodeType) + " (" + ToString(binary.Right) + ")";

        string ToString(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.Add: return "+";//TODO@@
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.And: return "AND";
                case ExpressionType.AndAlso: return "AND";
                case ExpressionType.Or: return "OR";
                case ExpressionType.OrElse: return "OR";
                //TODO NOT
            }
            throw new NotImplementedException();
        }

        string ToString(ConstantExpression constant)
        {
            var func = Expression.Lambda(constant).Compile();
            return ToStringObject(func.DynamicInvoke());
        }

        string ToString(MemberExpression member)
        {
            var name = GetElementName(member);
            TableInfo table;
            if (_dbInfo.GetLambdaNameAndTable().TryGetValue(name, out table))
            {
                return table.SqlFullName;
            }
            ColumnInfo col;
            if (_dbInfo.GetLambdaNameAndColumn().TryGetValue(name, out col))
            {
                return col.SqlFullName;
            }
            var func = Expression.Lambda(member).Compile();
            return ToStringObject(func.DynamicInvoke().ToString());
        }

        static string GetElementName(MemberExpression exp)
        {
            //TODO I'll make best code.
            return string.Join(".", exp.ToString().Split('.').Skip(1).ToArray());
        }

        internal string ToStringObject(object obj)
        {
            var exp = obj as Expression;
            if (exp != null)
            {
                return ToString(exp);
            }
            Type type = obj.GetType();
            if (type == typeof(string) || type == typeof(DateTime))
            {
                return "'" + obj + "'";
            }
            return obj.ToString();
        }

        internal string MakeSqlArguments(IEnumerable<object> src)
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