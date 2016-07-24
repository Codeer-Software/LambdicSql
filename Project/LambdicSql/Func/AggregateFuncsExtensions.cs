using LambdicSql.QueryBase;
using System;

namespace LambdicSql
{
    public static class AggregateFuncsExtensions
    {
        public static T Sum<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Sum<T>(this ISqlFunc func, AggregatePredicate aggregatePredicate, T item) { return default(T); }
        public static T Count<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Count<T>(this ISqlFunc func, AggregatePredicate aggregatePredicate, T item) { return default(T); }
        public static T Avg<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Min<T>(this ISqlFunc func, T item) { return default(T); }
        public static T Max<T>(this ISqlFunc func, T item) { return default(T); }

        public static string CusotmInvoke(Type returnType, string name, DecodedInfo[] argSrc)
        {
            if (argSrc.Length != 2) return null;
            return name + "(" + argSrc[0].Text.ToUpper() + " " + argSrc[1].Text + ")";
        }
    }
}
