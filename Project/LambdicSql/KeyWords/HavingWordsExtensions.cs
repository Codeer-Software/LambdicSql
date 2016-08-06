﻿using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class HavingWordsExtensions
    {
        public static ISqlKeyWord<TSelected> Having<TSelected>(this ISqlKeyWord<TSelected> words, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Having));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = converter.ToString(method.Arguments[method.SqlSyntaxMethodArgumentAdjuster()(0)]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "HAVING " + text;
        }
    }
}
