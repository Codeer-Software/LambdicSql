﻿using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    static class ExpressionToSqlString
    {
        internal static string GetElementName<TDB, T>(Expression<Func<TDB, T>> exp) where TDB : class
        {
            return GetElementName(exp.Body as MemberExpression);
        }

        internal static string ToString(DbInfo info, Expression exp)
        {
            var member = exp as MemberExpression;
            if (member != null) return ToString(info, member);

            var constant = exp as ConstantExpression;
            if (constant != null) return ToString(constant);

            var binary = exp as BinaryExpression;
            if (binary != null) return ToString(info, binary);

            var method = exp as MethodCallExpression;
            if (method != null) return ToString(info, method);

            var unary = exp as UnaryExpression;
            if (unary != null) return ToString(info, unary);

            throw new NotSupportedException();
        }

        static string ToString(DbInfo info, UnaryExpression unary)
          => ToString(info, unary.Operand);

        static string ToString(DbInfo info, MethodCallExpression method)
        {
            var arguments = new List<string>();
            foreach (var arg in method.Arguments.Skip(1))
            {
                arguments.Add(ToString(info, arg));
            }
            return method.Method.Name + "(" + string.Join(", ", arguments) + ")";
        }

        static string ToString(DbInfo info, BinaryExpression binary)
            => "(" + ToString(info, binary.Left) + ") " + ToString(binary.NodeType) + " (" + ToString(info, binary.Right) + ")";

        static string ToString(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "!=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.And: return "AND";
                case ExpressionType.AndAlso: return "AND";
                case ExpressionType.Or: return "OR";
                case ExpressionType.OrElse: return "OR";
            }
            throw new NotImplementedException();
        }

        static string ToString(ConstantExpression constant)
        {
            dynamic func = Expression.Lambda(constant).Compile();
            return "'" + func().ToString() + "'";
        }

        static string ToString(DbInfo info, MemberExpression member)
        {
            var name = GetElementName(member);
            TableInfo table;
            if (info.LambdaNameAndTable.TryGetValue(name, out table))
            {
                return table.SqlFullName;
            }
            ColumnInfo col;
            if (info.LambdaNameAndColumn.TryGetValue(name, out col))
            {
                return col.SqlFullName;
            }
            dynamic func = Expression.Lambda(member).Compile();
            return "'" + func().ToString() + "'";
        }

        static string GetElementName(MemberExpression exp)
        {
            //TODO @I'll make best code.
            return string.Join(".", exp.ToString().Split('.').Skip(1));
        }
    }
}
