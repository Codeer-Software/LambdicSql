using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql
{
    //TODO Integrate Keyword, Func, Window, Utils

    /// <summary>
    /// SQL Window functions.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    [SqlSyntax]
    public static class Window
    {
        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T SumOver<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(SumOver));

        /// <summary>
        /// SUM function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>Total.</returns>
        [MethodGroup(nameof(Window))]
        public static T SumOver<T>(AggregatePredicate aggregatePredicate, T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(SumOver));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static int CountOver(object column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(CountOver));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="asterisk"></param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static int CountOver(IAsterisk asterisk, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(CountOver));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static int CountOver(AggregatePredicate aggregatePredicate, object column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(CountOver));

        /// <summary>
        /// COUNT function.
        /// </summary>
        /// <param name="aggregatePredicate">Specify All or Distinct.</param>
        /// <param name="asterisk">*</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>Count.</returns>
        [MethodGroup(nameof(Window))]
        public static int CountOver(AggregatePredicate aggregatePredicate, IAsterisk asterisk, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(CountOver));

        /// <summary>
        /// AVG function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static double AvgOver(object column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<double>(nameof(AvgOver));

        /// <summary>
        /// MIN function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T MinOver<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(MinOver));

        /// <summary>
        /// MAX function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T MaxOver<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(MaxOver));

        /// <summary>
        /// FIRST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T First_Value_Over<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(First_Value_Over));

        /// <summary>
        /// LAST_VALUE function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T Last_Value_Over<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(Last_Value_Over));

        /// <summary>
        /// RANK function.
        /// </summary>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static int RankOver(IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(RankOver));

        /// <summary>
        /// DENSE_RANK function.
        /// </summary>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static int Dense_Rank_Over(IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(Dense_Rank_Over));

        /// <summary>
        /// PERCENT_RANK function.
        /// </summary>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static double Percent_Rank_Over(IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<double>(nameof(Percent_Rank_Over));

        /// <summary>
        /// CUME_DIST function.
        /// </summary>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static double Cume_Dist_Over(IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<double>(nameof(Cume_Dist_Over));

        /// <summary>
        /// NTILE function.
        /// </summary>
        /// <param name="groupCount">The number of ranking groups.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static int NtileOver(object groupCount, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(NtileOver));

        /// <summary>
        /// NTH_VALUE function.
        /// </summary>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">Specify the number of lines associated with the first line of the window that returns the expression.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static int Nth_Value_Over(object column, object offset, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<int>(nameof(Nth_Value_Over));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T LagOver<T>(T column, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(LagOver));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T LagOver<T>(T column, object offset, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(LagOver));

        /// <summary>
        /// LAG function.
        /// </summary>
        /// <typeparam name="T">Type represented by expression.</typeparam>
        /// <param name="column">The column or expression that is function target.</param>
        /// <param name="offset">An optional parameter that specifies the number of lines before the current line that returns a value.</param>
        /// <param name="default">The value returned if the value specified by offset is NULL.</param>
        /// <param name="partitionBy">PARTITION BY keyword.</param>
        /// <param name="orderBy">ORDER BY keyword.</param>
        /// <param name="rows">ROWS keyword.</param>
        /// <returns>It is an object for describing the continuation of the OVER expression.</returns>
        [MethodGroup(nameof(Window))]
        public static T LagOver<T>(T column, object offset, T @default, IPartitionBy partitionBy, IOrderBy orderBy, IRows rows) => InvalitContext.Throw<T>(nameof(LagOver));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        public static IRows Rows(int preceding) => InvalitContext.Throw<IRows>(nameof(Rows));

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="preceding">Preceding row count.</param>
        /// <param name="following">Following row count.</param>
        public static IRows Rows(int preceding, int following) => InvalitContext.Throw<IRows>(nameof(Rows));

        static SqlText ConvertRows(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];
            var args = exp.Arguments.Select(e => converter.Convert(e)).ToArray();

            //Sql server can't use parameter.
            if (exp.Arguments.Count == 1)
            {
                return LineSpace("ROWS", args[0].Customize(new CustomizeParameterToObject()), "PRECEDING");
            }
            else
            {
                return LineSpace("ROWS BETWEEN", args[0].Customize(new CustomizeParameterToObject()),
                    "PRECEDING AND", args[1].Customize(new CustomizeParameterToObject()), "FOLLOWING");
            }
        }

        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        public static IPartitionBy PartitionBy(params object[] columns) => InvalitContext.Throw<IPartitionBy>(nameof(PartitionBy));

        static SqlText ConvertPartitionBy(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];

            var partitionBy = new VText();
            partitionBy.Add("PARTITION BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = exp.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }
        /*
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        public static IOrderBy OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<IOrderBy>(nameof(OrderBy));

        static SqlText ConvertOrderBy(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var exp = methods[0];
            var orderBy = new VText();
            orderBy.Add("ORDER BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = exp.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            orderBy.Add(elements);

            return orderBy;
        }*/

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var v = new VText();
            var argCount = methods[0].Arguments.Count - 3;

            //TODO refactoring.
            //あーなるほど、nullはここで止めてたのか。
            var method = methods[0];

            switch (methods[0].Method.Name)
            {
                case nameof(Rows):
                    return ConvertRows(converter, methods);
                case nameof(PartitionBy):
                    return ConvertPartitionBy(converter, methods);
                case nameof(SumOver):
                case nameof(CountOver):
                    var args = method.Arguments.Select(e => converter.Convert(e));
                    if (method.Arguments.Count == 5) v.Add(FuncSpace(method.Method.Name.ToUpper().Replace("OVER", string.Empty), args.Take(2).ToArray()));
                    else v.Add(FuncSpace(method.Method.Name.ToUpper().Replace("OVER", string.Empty), args.Take(1).ToArray()));
                    break;
                default:
                    v.Add(Func(method.Method.Name.ToUpper().Replace("_OVER", string.Empty).Replace("OVER", string.Empty), method.Arguments.Take(argCount).Select(e => converter.Convert(e)).ToArray()));
                    break;
            }
            v.Add("OVER(");
            v.AddRange(1, methods[0].Arguments.Skip(argCount).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}
