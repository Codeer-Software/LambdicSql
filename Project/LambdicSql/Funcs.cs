using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// SQL Functions.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Funcs;], you can be used to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Funcs
    {
        /// <summary>
        /// Calculate total.
        /// </summary>
        /// <typeparam name="T">Type represented by expression</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Sum<T>(T expression) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// Calculate total.
        /// </summary>
        /// <typeparam name="T">Type represented by expression</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T expression) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int Count(object item) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ast"></param>
        /// <returns></returns>
        public static int Count(Asterisk ast) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregatePredicate"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int Count(AggregatePredicate aggregatePredicate, object item) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T Avg<T>(T item) => InvalitContext.Throw<T>(nameof(Avg));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T Min<T>(T item) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T Max<T>(T item) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Abs<T>(T src) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        public static T Mod<T>(T target, int div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static T Round<T>(T src, int digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Concat(params string[] values) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Length(string target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Len(string target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Lower(string target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Upper(string target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static string Replace(string target, string src, string dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Substring(string target, int startIndex, int length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDate() => InvalitContext.Throw<DateTime>(nameof(CurrentDate));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CurrentTime<T>() => InvalitContext.Throw<T>(nameof(DateTimeOffset));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentTimeStamp));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentSpaceDate() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceDate));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TimeSpan CurrentSpaceTime() => InvalitContext.Throw<TimeSpan>(nameof(CurrentSpaceTime));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentSpaceTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceTimeStamp));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Extract<T>(DateTiemElement element, DateTime src) => InvalitContext.Throw<T>(nameof(Extract));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int DatePart(DateTiemElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDst"></typeparam>
        /// <param name="src"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static TDst Cast<TDst>(object src, string dataType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Coalesce<T>(params T[] args) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
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
                    return "CAST((" + args[0] + ") AS " + converter.Context.Parameters.ResolvePrepare(args[1]) + ")";
            }
            return method.Method.Name.ToUpper() + "(" + string.Join(", ", args.Skip(method.AdjustSqlSyntaxMethodArgumentIndex(0)).ToArray()) + ")";
        }
    }
}
