﻿using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class NullCheck
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(LambdicSql.Keywords.IsNull):
                    return converter.ToString(method.Arguments[0]) + " IS NULL";
                case nameof(LambdicSql.Keywords.IsNotNull):
                    return converter.ToString(method.Arguments[0]) + " IS NOT NULL";
            }
            throw new NotSupportedException();
        }
    }
}