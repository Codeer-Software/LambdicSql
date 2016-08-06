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
        public interface IThenAfter : ISqlChainingKeyWord { }
        public interface IElseAfter : ISqlChainingKeyWord { }
        public interface IEndAfter : ISqlChainingKeyWord { }
        
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IWhenAfter When<T>(this IThenAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IThenAfter Then<T>(this IWhenAfter words, T t) => InvalitContext.Throw<IThenAfter>(nameof(Then));
        public static IElseAfter Else<T>(this IThenAfter words, T t) => InvalitContext.Throw<IElseAfter>(nameof(Then));
        public static IEndAfter End(this IThenAfter words) => InvalitContext.Throw<IEndAfter>(nameof(End));
        public static IEndAfter End(this IElseAfter words) => InvalitContext.Throw<IEndAfter>(nameof(End));

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
