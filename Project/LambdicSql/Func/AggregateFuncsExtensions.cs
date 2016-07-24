using LambdicSql.QueryBase;
using System;

namespace LambdicSql
{
    public static class AggregateFuncsExtensions
    {
        public static T Sum<T>(this ISqlFuncs func, T item) { return default(T); }
        public static T Sum<T>(this ISqlFuncs func, AggregatePredicate aggregatePredicate, T item) { return default(T); }
        public static T Count<T>(this ISqlFuncs func, T item) { return default(T); }
        public static T Count<T>(this ISqlFuncs func, AggregatePredicate aggregatePredicate, T item) { return default(T); }
        public static T Avg<T>(this ISqlFuncs func, T item) { return default(T); }
        public static T Min<T>(this ISqlFuncs func, T item) { return default(T); }
        public static T Max<T>(this ISqlFuncs func, T item) { return default(T); }

        public static string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc)
        {
            if (argSrc.Length != 2) return null;
            return name + "(" + argSrc[0].Text.ToUpper() + " " + argSrc[1].Text + ")";
        }
    }
}
