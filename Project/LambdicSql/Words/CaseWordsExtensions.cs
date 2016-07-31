using LambdicSql.QueryBase;
using System;

namespace LambdicSql
{
    public static class CaseWordsExtensions
    {
        public interface ICaseAfter : ISqlWordsChaining { }
        public interface IWhenAfter : ISqlWordsChaining { }
        public interface IThenAfter : ISqlWordsChaining { }
        public interface IElseAfter : ISqlWordsChaining { }
        public interface IEndAfter : ISqlWordsChaining { }

        public static ICaseAfter Case(this ISqlWords words) => null;
        public static ICaseAfter Case<T>(this ISqlWords words, T t) => null;
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => null;
        public static IWhenAfter When<T>(this IThenAfter words, T t) => null;
        public static IThenAfter Then<T>(this IWhenAfter words, T t) => null;
        public static IElseAfter Else<T>(this IThenAfter words, T t) => null;
        public static IEndAfter End(this IThenAfter words) => null;
        public static IEndAfter End(this IElseAfter words) => null;
        public static T Cast<T>(this ISqlWords words) => default(T);

        public static string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc)
        {
            switch (name)
            {
                case nameof(Case):
                    {
                        var text = Environment.NewLine + "\tCASE";
                        if (argSrc.Length == 1)
                        {
                            text += (" " + argSrc[0].Text);
                        }
                        return text;
                    }
                case nameof(When): return Environment.NewLine + "\t\tWHEN " + argSrc[0].Text;
                case nameof(Then): return " THEN " + argSrc[0].Text;
                case nameof(Else): return Environment.NewLine + "\t\tELSE " + argSrc[0].Text;
                case nameof(End): return Environment.NewLine + "\tEND";
                case nameof(Cast): return string.Empty;
            }
            return null;
        }
    }
}
