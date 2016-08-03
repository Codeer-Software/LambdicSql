using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace LambdicSql
{
    public static class CaseWordsExtensions
    {
        public interface ICaseAfter : ISqlSyntax { }
        public interface IWhenAfter : ISqlSyntax { }
        public interface IThenAfter : ISqlSyntax { }
        public interface IElseAfter : ISqlSyntax { }
        public interface IEndAfter : ISqlSyntax { }

        public static ICaseAfter Case(this ISqlSyntax words) => null;
        public static ICaseAfter Case<T>(this ISqlSyntax words, T t) => null;
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => null;
        public static IWhenAfter When<T>(this IThenAfter words, T t) => null;
        public static IThenAfter Then<T>(this IWhenAfter words, T t) => null;
        public static IElseAfter Else<T>(this IThenAfter words, T t) => null;
        public static IEndAfter End(this IThenAfter words) => null;
        public static IEndAfter End(this IElseAfter words) => null;
        public static T Cast<T>(this ISqlSyntax words) => default(T);

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            foreach (var m in methods)
            {
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(m.Method.Name, argSrc));
            }
            return string.Join(string.Empty, list.ToArray());
        }

        static string MethodToString(string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Case):
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
                case nameof(Cast): return string.Empty;
            }
            return null;
        }
    }


    public static class WindowWordsExtensions
    {
        public static IWindowFunctionsAfter AvgOver<T>(this IWindowFunctions words, T t) => null;
        public static IWindowFunctionsAfter LagOver<T>(this IWindowFunctions words, T t) => null;
        public static IWindowFunctionsAfter LagOver<T>(this IWindowFunctions words, T t, object offset) => null;
        public static IWindowFunctionsAfter LagOver<T>(this IWindowFunctions words, T t, object offset, object @default) => null;
        public static IWindowFunctionsAfter PartitionBy(this IWindowFunctionsAfter words, params object[] t) => null;
        public static IWindowFunctionsAfter OrderBy(this IWindowFunctionsAfter words) => null;
        public static IWindowFunctionsAfter Asc<T>(this IWindowFunctionsAfter words, T t) => null;
        public static IWindowFunctionsAfter Desc<T>(this IWindowFunctionsAfter words, T t) => null;
        public static IWindowFunctionsAfter Rows<T>(this IWindowFunctionsAfter words, T t) => null;
        public static IWindowFunctionsAfter Rows<T>(this IWindowFunctionsAfter words, T t, T t2) => null;
        public static T Cast<T>(this IWindowFunctionsAfter words) => default(T);

        public static string MethodChainToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var list = new List<string>();
            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                var argSrc = m.Arguments.Skip(1).Select(e => converter.ToString(e)).ToArray();
                list.Add(MethodToString(converter, m.Method.Name, argSrc));
                if (i + 1 < methods.Length)
                {
                    switch (methods[i].Method.Name)
                    {
                        case nameof(Asc):
                        case nameof(Desc):
                            switch (methods[i + 1].Method.Name)
                            {
                                case nameof(Asc):
                                case nameof(Desc):
                                    list.Add(", ");
                                    break;
                            }
                            break;
                    }
                }
            }
            return string.Join(string.Empty, list.ToArray()) + ")";
        }

        static string MethodToString(ISqlStringConverter converter, string name, string[] argSrc)
        {
            switch (name)
            {
                case nameof(Cast): return string.Empty;
                case nameof(PartitionBy): return Environment.NewLine + "\t" + "PARTITION BY" + " " + argSrc[0];
                case nameof(OrderBy): return Environment.NewLine + "\t" + "ORDER BY";
                case nameof(Asc): return Environment.NewLine + "\t\t" + argSrc[0] + " ASC";
                case nameof(Desc): return Environment.NewLine + "\t\t" + argSrc[0] + " DESC";
                case nameof(Rows):
                    {
                        if (argSrc.Length == 1)
                        {
                            return Environment.NewLine + "\tROWS " + argSrc[0] + " PRECEDING";
                        }
                        else
                        {
                            return Environment.NewLine + "\tROWS BETWEEN " + converter.ResolvePrepare(argSrc[0]) +
                                " PRECEDING AND " + converter.ResolvePrepare(argSrc[1]) + " FOLLOWING";
                        }
                    }
            }
            return Environment.NewLine + "\t" + name.ToUpper().Replace("OVER", string.Empty)
                + "(" + string.Join(", ", argSrc) + ") OVER(";
        }
    }
}
