﻿using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class UpdateWordsExtensions
    {
        public static ISqlWords<TSelected> Update<TSelected>(this ISqlWords<TSelected> words, object table) => null;
        public static ISqlWords<TSelected> Set<TSelected>(this ISqlWords<TSelected> words) => null;
        public static ISqlWords<TSelected> Assign<TSelected>(this ISqlWords<TSelected> words, object target, object value) => null;

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
                if (i + 1 < methods.Length)
                {
                    switch (methods[i].Method.Name)
                    {
                        case nameof(Assign):
                            switch (methods[i + 1].Method.Name)
                            {
                                case nameof(Assign):
                                    list.Add(", ");
                                    break;
                            }
                            break;
                    }
                }
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Update): return Environment.NewLine + "UPDATE " + argSrc[0];
                case nameof(Set): return Environment.NewLine + "SET";
                case nameof(Assign): return Environment.NewLine + "\t" + MemberNameOnly(argSrc[0]) + " = " + argSrc[1];
            }
            throw new NotSupportedException();
        }

        static string MemberNameOnly(string src)
        {
            var index = src.LastIndexOf(".");
            return index == -1 ? src : src.Substring(index + 1);
        }
    }
}