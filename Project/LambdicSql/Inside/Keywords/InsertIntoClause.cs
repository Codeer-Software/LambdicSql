﻿using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class InsertIntoClause
    {
        internal static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(m.AdjustSqlSyntaxMethodArgumentIndex(0)).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(LambdicSql.Keywords.InsertInto):
                    {
                        var arg = argSrc.Last().Split(',').Select(e => GetColumnOnly(e)).ToArray();
                        return Environment.NewLine + "INSERT INTO " + argSrc[0] + "(" + string.Join(", ", arg) + ")";

                    }
                case nameof(LambdicSql.Keywords.Values): return Environment.NewLine + "\tVALUES (" + string.Join(", ", argSrc) + ")";
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
