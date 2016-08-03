﻿using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class HavingWordsExtensions
    {
        public static ISqlWords<TSelected> Having<TSelected>(this ISqlWords<TSelected> words, bool condition) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = converter.ToString(method.Arguments[1]);
            return string.IsNullOrEmpty(text.Trim()) ? string.Empty : Environment.NewLine + "HAVING " + converter.ToString(method.Arguments[1]);
        }
    }
}