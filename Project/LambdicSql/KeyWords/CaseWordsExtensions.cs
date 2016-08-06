using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using LambdicSql.Inside;

namespace LambdicSql
{
    public static class CaseWordsExtensions
    {
        public interface ICaseAfter : ISqlChainingKeyWord { }
        public interface IWhenAfter : ISqlChainingKeyWord { }
        public interface IWhenAfter<T> : ISqlChainingKeyWord { }
        public interface IThenAfter<T> : ISqlChainingKeyWord { }
        public interface IElseAfter<T> : ISqlChainingKeyWord { }
        
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IWhenAfter<T1> When<T1, T2>(this IThenAfter<T1> words, T2 t) => InvalitContext.Throw<IWhenAfter<T1>>(nameof(When));
        public static IThenAfter<T> Then<T>(this IWhenAfter words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IElseAfter<T> Else<T>(this IThenAfter<T> words, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));
        public static T End<T>(this IThenAfter<T> words) => InvalitContext.Throw<T>(nameof(End));
        public static T End<T>(this IElseAfter<T> words) => InvalitContext.Throw<T>(nameof(End));

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
                case nameof(Sql.Case):
                    {
                        var text = Environment.NewLine + "\tCASE";
                        if (argSrc.Length == 1)
                        {
                            text += (" " + argSrc[0]);
                        }
                        return text;
                    }
                case nameof(When): return Environment.NewLine + "\t\tWHEN " + argSrc[0];
                case nameof(Then): return " THEN " + argSrc[0];
                case nameof(Else): return Environment.NewLine + "\t\tELSE " + argSrc[0];
                case nameof(End): return Environment.NewLine + "\tEND";
            }
            return null;
        }
    }
}
