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
    /// Use[using static LambdicSql.Funcs;], you can use to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Funcs
    {
        /// <summary>
        /// Calculate total.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Sum<T>(T expression) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// Calculate total.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Sum<T>(AggregatePredicate aggregatePredicate, T expression) => InvalitContext.Throw<T>(nameof(Sum));

        /// <summary>
        /// Count the number of expression.
        /// </summary>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static int Count(object expression) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// Count the number of row.
        /// </summary>
        /// <param name="asterisk"></param>
        /// <returns></returns>
        public static int Count(Asterisk asterisk) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// Count the number of expression.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static int Count(AggregatePredicate aggregatePredicate, object expression) => InvalitContext.Throw<int>(nameof(Count));

        /// <summary>
        /// Calculate average of expression.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Avg<T>(T expression) => InvalitContext.Throw<T>(nameof(Avg));

        /// <summary>
        /// Get minimum item of expression.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Min<T>(T expression) => InvalitContext.Throw<T>(nameof(Min));

        /// <summary>
        /// Get maximum item of expression.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Max<T>(T expression) => InvalitContext.Throw<T>(nameof(Max));

        /// <summary>
        /// Calculate absolute value of expression.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression">It specifies constants, columns, functions, and combinations of arithmetic operators, bit operators, and string operators.</param>
        /// <returns></returns>
        public static T Abs<T>(T expression) => InvalitContext.Throw<T>(nameof(Abs));

        /// <summary>
        /// Calculate surplus.
        /// </summary>
        /// <typeparam name="T">Type represented by target</typeparam>
        /// <param name="target">Numeric expression to divide.</param>
        /// <param name="div">A numeric expression that divides the dividend.</param>
        /// <returns>Surplus.</returns>
        public static T Mod<T>(T target, int div) => InvalitContext.Throw<T>(nameof(Mod));

        /// <summary>
        /// Returns a numeric value, rounded to the specified length or precision.
        /// </summary>
        /// <typeparam name="T">Type represented by target.</typeparam>
        /// <param name="target">Is an expression of the exact numeric or approximate numeric data type category, except for the bit data type.</param>
        /// <param name="digit">Is the precision to which it is to be rounded.</param>
        /// <returns>Rounded result.</returns>
        public static T Round<T>(T target, int digit) => InvalitContext.Throw<T>(nameof(Round));

        /// <summary>
        /// Returns a string that is the result of concatenating two or more string values.
        /// </summary>
        /// <param name="targets">A string value to concatenate to the other values.</param>
        /// <returns>concatenated result.</returns>
        public static string Concat(params string[] targets) => InvalitContext.Throw<string>(nameof(Concat));

        /// <summary>
        /// Returns the number of characters of the specified string expression, excluding trailing blanks.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Length(string target) => InvalitContext.Throw<int>(nameof(Length));

        /// <summary>
        /// Returns the number of characters of the specified string expression, excluding trailing blanks.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>String length.</returns>
        public static int Len(string target) => InvalitContext.Throw<int>(nameof(Len));

        /// <summary>
        /// Returns a character expression after converting uppercase character data to lowercase.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Lower(string target) => InvalitContext.Throw<string>(nameof(Lower));

        /// <summary>
        /// Returns a character expression with lowercase character data converted to uppercase.
        /// </summary>
        /// <param name="target">target.</param>
        /// <returns>Changed string.</returns>
        public static string Upper(string target) => InvalitContext.Throw<string>(nameof(Upper));

        /// <summary>
        /// Replaces all occurrences of a specified string value with another string value.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="src">source.</param>
        /// <param name="dst">destination.</param>
        /// <returns>Changed string.</returns>
        public static string Replace(string target, string src, string dst) => InvalitContext.Throw<string>(nameof(Replace));

        /// <summary>
        /// Returns part of a text.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="startIndex">Specify the starting position of the character string to be acquired.</param>
        /// <param name="length">Specify the length of the string to be retrieved.</param>
        /// <returns>Part of a text.</returns>
        public static string Substring(string target, int startIndex, int length) => InvalitContext.Throw<string>(nameof(Substring));

        /// <summary>
        /// Get date of executing SQL.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        public static DateTime CurrentDate() => InvalitContext.Throw<DateTime>(nameof(CurrentDate));

        /// <summary>
        /// Get time of executing SQL.
        /// </summary>
        /// <returns>Date of executing SQL.</returns>
        public static TimeSpan CurrentTime() => InvalitContext.Throw<TimeSpan>(nameof(DateTimeOffset));

        /// <summary>
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime CurrentTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentTimeStamp));

        /// <summary>
        /// CURRENT DATE.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime CurrentSpaceDate() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceDate));

        /// <summary>
        /// CURRENT TIME.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static TimeSpan CurrentSpaceTime() => InvalitContext.Throw<TimeSpan>(nameof(CurrentSpaceTime));

        /// <summary>
        /// CURRENT TIMESTAMP.
        /// Get date and time of executing SQL.
        /// </summary>
        /// <returns>Date and time of executing SQL.</returns>
        public static DateTime CurrentSpaceTimeStamp() => InvalitContext.Throw<DateTime>(nameof(CurrentSpaceTimeStamp));

        /// <summary>
        /// Extract a part from the date data.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static double Extract(DateTimeElement element, DateTime src) => InvalitContext.Throw<double>(nameof(Extract));

        /// <summary>
        /// Extract a part from the date data.
        /// </summary>
        /// <param name="element">Part type.</param>
        /// <param name="src">The date data.</param>
        /// <returns>A part from the date data.</returns>
        public static int DatePart(DateTimeElement element, DateTime src) => InvalitContext.Throw<int>(nameof(Extract));

        /// <summary>
        /// Convert a type.
        /// </summary>
        /// <typeparam name="TDst">Type of destination.</typeparam>
        /// <param name="target"></param>
        /// <param name="destinationType">Type of destination.</param>
        /// <returns>Converted data.</returns>
        public static TDst Cast<TDst>(object target, string destinationType) => InvalitContext.Throw<TDst>(nameof(Cast));

        /// <summary>
        /// Returns the first non-null value in the parameter.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <returns>The first non-null value in the parameter.</returns>
        public static T Coalesce<T>(params T[] parameter) => InvalitContext.Throw<T>(nameof(Coalesce));

        /// <summary>
        /// If expression1 is null, it returns expression2. Otherwise it returns expression1.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="expression1">expression.</param>
        /// <param name="expression1">expression.</param>
        /// <returns>expression1 or expression2.</returns>
        public static T NVL<T>(T expression1, T expression2) => InvalitContext.Throw<T>(nameof(NVL));

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
