using LambdicSql.QueryBase;
using System;

namespace LambdicSql
{
    public static class DistinctExtensions
    {
        public static T Distinct<T>(this ISqlWord word, T target) => default(T);

        public static string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc)
            => nameof(Distinct).ToUpper() + " " + argSrc[0].Text;
    }
}
