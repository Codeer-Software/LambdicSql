using LambdicSql.QueryBase;
using System;
using System.Linq;

namespace LambdicSql
{
    public static class WordsExtensions
    {
        public static bool Like(this ISqlWords words, string target, string serachText) => false;
        public static bool Between<TTarget>(this ISqlWords words, TTarget target, TTarget min, TTarget max) => false;
        public static bool In<TTarget>(this ISqlWords words, TTarget target, params TTarget[] inArguments) => false;

        public static string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc)
        {
            switch (name)
            {
                case nameof(Like): return argSrc[0].Text + " LIKE " + argSrc[1].Text;
                case nameof(Between): return argSrc[0].Text + " BETWEEN " + argSrc[1].Text + " AND " + argSrc[2].Text;
                case nameof(In): return argSrc[0].Text + " IN(" + string.Join(", ", argSrc.Skip(1).Select(e => e.Text).ToArray()) + ")";
            }
            return null;
        }
    }
}
