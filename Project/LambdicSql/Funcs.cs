using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public static class Funcs
    {
        public static T Sum<T>(T item) => InvalitContext.Throw<T>(nameof(Sum));
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T item) => InvalitContext.Throw<T>(nameof(Sum));
        public static int Count(object item) => InvalitContext.Throw<int>(nameof(Count));
        public static int Count(Asterisk ast) => InvalitContext.Throw<int>(nameof(Count));
        public static int Count(AggregatePredicate aggregatePredicate, object item) => InvalitContext.Throw<int>(nameof(Count));
        public static T Avg<T>(T item) => InvalitContext.Throw<T>(nameof(Avg));
        public static T Min<T>(T item) => InvalitContext.Throw<T>(nameof(Min));
        public static T Max<T>(T item) => InvalitContext.Throw<T>(nameof(Max));

        public static T Abs<T>(T src) => InvalitContext.Throw<T>(nameof(Abs));
        public static T Mod<T>(T target, int div) => InvalitContext.Throw<T>(nameof(Mod));
        public static T Round<T>(T src, int digit) => InvalitContext.Throw<T>(nameof(Round));

        public static string Concat(params string[] values) => InvalitContext.Throw<string>(nameof(Concat));
        public static int Length(string target) => InvalitContext.Throw<int>(nameof(Length));
        public static int Len(string target) => InvalitContext.Throw<int>(nameof(Len));
        public static string Lower(string target) => InvalitContext.Throw<string>(nameof(Lower));
        public static string Upper(string target) => InvalitContext.Throw<string>(nameof(Upper));
        public static string Replace(string target, string src, string dst) => InvalitContext.Throw<string>(nameof(Replace));
        public static string Substring(string target, int startIndex, int length) => InvalitContext.Throw<string>(nameof(Substring));

        public static DateTime CurrentDate() => InvalitContext.Throw<DateTime>(nameof(CurrentDate));
        public static T CurrentTime<T>() => InvalitContext.Throw<T>(nameof(DateTimeOffset));
        public static DateTime CurrentTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentTimeStamp));

        public static DateTime CurrentSpaceDate() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceDate));
        public static TimeSpan CurrentSpaceTime() => InvalitContext.Throw<TimeSpan>(nameof(CurrentSpaceTime));
        public static DateTime CurrentSpaceTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceTimeStamp));
        public static T Extract<T>(DateTiemElement element, DateTime src) => InvalitContext.Throw<T>(nameof(Extract));
        public static int DatePart(DateTiemElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        public static TDst Cast<TDst>(object src, string dataType) => InvalitContext.Throw<TDst>(nameof(Cast));

        public static T Coalesce<T>(params T[] args) => InvalitContext.Throw<T>(nameof(Coalesce));
        public static T NVL<T>(T t1, T t2) => InvalitContext.Throw<T>(nameof(NVL));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var args = method.Arguments.Select(e => converter.ToString(e)).ToArray();
            switch (method.Method.Name)
            {
                case nameof(Sum):
                case nameof(Count):
                    if (method.Arguments.Count == 2) return method.Method.Name.ToUpper() + "(" + args[0].ToUpper() + " " + args[1] + ")";
                    break;
                case nameof(CurrentDate):
                    return "CURRENT_DATE";
                case nameof(CurrentTime):
                    return "CURRENT_TIME";
                case nameof(CurrentTimeStamp):
                    return "CURRENT_TIMESTAMP";
                case nameof(CurrentSpaceDate):
                    return "CURRENT DATE";
                case nameof(CurrentSpaceTime):
                    return "CURRENT TIME";
                case nameof(CurrentSpaceTimeStamp):
                    return "CURRENT TIMESTAMP";
                case nameof(Extract):
                    return method.Method.Name.ToUpper() + "(" + args[0].ToUpper() + " FROM " + args[1] + ")";
                case nameof(DatePart):
                    return method.Method.Name.ToUpper() + "(" + args[0].ToUpper() + ", " + args[1] + ")";
                case nameof(Cast):
                    return "CAST(" + args[0] + " AS " + converter.Context.Parameters.ResolvePrepare(args[1]) + ")";
            }
            return method.Method.Name.ToUpper() + "(" + string.Join(", ", args.Skip(method.AdjustSqlSyntaxMethodArgumentIndex(0)).ToArray()) + ")";
        }
    }
}
