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
}
