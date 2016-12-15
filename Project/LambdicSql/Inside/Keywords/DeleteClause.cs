﻿using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class DeleteClause
    {
        internal static TextParts Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
            => methods[0].Method.Name.ToUpper();
    }
}
