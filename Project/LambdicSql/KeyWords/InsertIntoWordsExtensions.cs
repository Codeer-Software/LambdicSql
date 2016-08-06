﻿using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class InsertIntoWordsExtensions
    {
        public interface IInsertIntoAfter<T> : ISqlChainingKeyWord<T> { }

        public static ISqlKeyWord<TSelected> Values<TSelected>(this IInsertIntoAfter<TSelected> words, params object[] targets)
             => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Values));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(m.SqlSyntaxMethodArgumentAdjuster()(0)).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Sql.InsertInto):
                    {
                        var arg = argSrc.Last().Split(',').Select(e => GetColumnOnly(e)).ToArray();
                        return Environment.NewLine + "INSERT INTO " + argSrc[0] + "(" + string.Join(", ", arg) + ")";

                    }
                case nameof(Values): return Environment.NewLine + "\tVALUES (" + string.Join(", ", argSrc) + ")";
            }
            throw new NotSupportedException();
        }

        static string GetColumnOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
    }
}
