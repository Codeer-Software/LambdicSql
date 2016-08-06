using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Window
{
    public static class WindowExtensions
    {
        public static IWindowFunctionsAfter Avg<T>(this IWindowFuncs words, T t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Avg));
        public static IWindowFunctionsAfter Lag<T>(this IWindowFuncs words, T t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Lag));
        public static IWindowFunctionsAfter Lag<T>(this IWindowFuncs words, T t, object offset) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Lag));
        public static IWindowFunctionsAfter Lag<T>(this IWindowFuncs words, T t, object offset, object @default) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Lag));
        public static IWindowFunctionsAfter Over(this IWindowFunctionsAfter words) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Over));
        public static IWindowFunctionsAfter PartitionBy(this IWindowFunctionsAfter words, params object[] t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(PartitionBy));
        public static IWindowFunctionsAfter OrderBy(this IWindowFunctionsAfter words) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(OrderBy));
        public static IWindowFunctionsAfter Asc<T>(this IWindowFunctionsAfter words, T t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Asc));
        public static IWindowFunctionsAfter Desc<T>(this IWindowFunctionsAfter words, T t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Desc));
        public static IWindowFunctionsAfter Rows<T>(this IWindowFunctionsAfter words, T t) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Rows));
        public static IWindowFunctionsAfter Rows<T>(this IWindowFunctionsAfter words, T t, T t2) => InvalitContext.Throw<IWindowFunctionsAfter>(nameof(Rows));
        public static T Cast<T>(this IWindowFunctionsAfter words) => default(T);

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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
                case nameof(Over): return Environment.NewLine + "\t" + name.ToUpper() + "(";
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
                            return Environment.NewLine + "\tROWS BETWEEN " + converter.Context.Parameters.ResolvePrepare(argSrc[0]) +
                                " PRECEDING AND " + converter.Context.Parameters.ResolvePrepare(argSrc[1]) + " FOLLOWING";
                        }
                    }
            }
            return Environment.NewLine + "\t" + name.ToUpper() + "(" + string.Join(", ", argSrc) + ")";
        }
    }
}
