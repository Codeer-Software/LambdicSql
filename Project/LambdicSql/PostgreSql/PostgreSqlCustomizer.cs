﻿using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.PostgreSql
{
    public class PostgreSqlCustomizer : IQueryCustomizer
    {
        public string CustomOperator(Type type1, string @operator, Type type2)
        {
            if ((type1 == typeof(string) || type2 == typeof(string)) && @operator == "+")
            {
                return "||";
            }
            return @operator;
        }
        public string CusotmSqlSyntax(ISqlStringConverter converter, MethodCallExpression[] methods) => null;
    }
}
