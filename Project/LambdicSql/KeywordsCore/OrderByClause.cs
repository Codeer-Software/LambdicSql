﻿using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    //TODO style change?
    public static class OrderByWordsClause
    {
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods.Skip(1))
            {
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return Environment.NewLine + "ORDER BY" + string.Join(",", list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Sql.OrderBy): return Environment.NewLine + "ORDER BY";
                case nameof(Sql.ASC): return Environment.NewLine + "\t" + argSrc[0] + " ASC";
                case nameof(Sql.DESC): return Environment.NewLine + "\t" + argSrc[0] + " DESC";
            }
            throw new NotSupportedException();
        }
    }
}
