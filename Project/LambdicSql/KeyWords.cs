using LambdicSql.Inside;
using LambdicSql.Inside.Keywords;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// SQL Keywords.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    [SqlSyntax]
    public static class Keywords
    {
        /// <summary>
        /// It's *.
        /// </summary>
        public static Asterisk Asterisk() => InvalitContext.Throw<Asterisk>(nameof(Asterisk));

        /// <summary>
        /// It's *.
        /// The type you want to obtain with the SELECT clause. Usually you specify a table element.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected"></param>
        /// <returns>*</returns>
        public static Asterisk<TSelected> Asterisk<TSelected>(TSelected selected) => InvalitContext.Throw<Asterisk<TSelected>>(nameof(Asterisk));

        /// <summary>
        /// This is TOP keywrod.
        /// </summary>
        /// <param name="count">Number of rows to return.</param>
        /// <returns>TOP clause.</returns>
        public static Top Top(long count) => InvalitContext.Throw<Top>(nameof(Top));

        /// <summary>
        /// This is ASC keywrod.
        /// </summary>
        /// <param name="target">target column.</param>
        /// <returns></returns>
        public static Asc Asc(object target) => InvalitContext.Throw<Asc>(nameof(Desc));

        /// <summary>
        /// This is DESC keywrod.
        /// </summary>
        /// <param name="target">target column.</param>
        /// <returns></returns>
        public static Desc Desc(object target) => InvalitContext.Throw<Desc>(nameof(Desc));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="top"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <param name="before"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <param name="before"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="selected"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(AggregatePredicate predicate, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select(AggregatePredicate predicate, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<Non> Select<TSrcSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, Asterisk asterisk) => InvalitContext.Throw<IQuery<Non>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        /// <summary>
        /// This is the SELECT clause.
        /// </summary>
        /// <typeparam name="TSrcSelected"></typeparam>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="before"></param>
        /// <param name="predicate"></param>
        /// <param name="top"></param>
        /// <param name="asterisk"></param>
        /// <returns>SQL Query. You can write SQL statements in succession, of course you can end it.</returns>
        public static IQuery<TSelected> Select<TSrcSelected, TSelected>(this IQuery<TSrcSelected> before, AggregatePredicate predicate, Top top, Asterisk<TSelected> asterisk) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));

        public interface ICaseAfter : IMethodChain { }
        public interface IWhenAfter : IMethodChain { }
        public interface IWhenAfter<T> : IMethodChain { }
        public interface IThenAfter<T> : IMethodChain { }
        public interface IElseAfter<T> : IMethodChain { }
        [MethodGroup(nameof(Case))]
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        [MethodGroup(nameof(Case))]
        public static ICaseAfter Case<T>(T t) => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        [MethodGroup(nameof(Case))]
        public static IWhenAfter When<T>(this ICaseAfter before, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        [MethodGroup(nameof(Case))]
        public static IWhenAfter<T1> When<T1, T2>(this IThenAfter<T1> before, T2 t) => InvalitContext.Throw<IWhenAfter<T1>>(nameof(When));
        [MethodGroup(nameof(Case))]
        public static IThenAfter<T> Then<T>(this IWhenAfter before, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        [MethodGroup(nameof(Case))]
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> before, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        [MethodGroup(nameof(Case))]
        public static IElseAfter<T> Else<T>(this IThenAfter<T> before, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));
        [MethodGroup(nameof(Case))]
        public static T End<T>(this IThenAfter<T> before) => InvalitContext.Throw<T>(nameof(End));
        [MethodGroup(nameof(Case))]
        public static T End<T>(this IElseAfter<T> before) => InvalitContext.Throw<T>(nameof(End));
        
        public static IQuery<Non> From(params object[] tbale) => InvalitContext.Throw<IQuery<Non>>(nameof(From));
        public static IQuery<TSelected> From<TSelected>(this IQuery<TSelected> before, params object[] tbale) => InvalitContext.Throw<IQuery<TSelected>>(nameof(From));

        public static IQuery<Non> Join<T>(T tbale, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Join));
        public static IQuery<TSelected> Join<TSelected, T>(this IQuery<TSelected> before, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Join));

        public static IQuery<Non> LeftJoin<T>(T tbale, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(LeftJoin));
        public static IQuery<TSelected> LeftJoin<TSelected, T>(this IQuery<TSelected> before, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(LeftJoin));

        public static IQuery<Non> RightJoin<T>(T tbale, bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(RightJoin));
        public static IQuery<TSelected> RightJoin<TSelected, T>(this IQuery<TSelected> before, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(RightJoin));

        public static IQuery<Non> CrossJoin<T>(T tbale) => InvalitContext.Throw<IQuery<Non>>(nameof(CrossJoin));
        public static IQuery<TSelected> CrossJoin<TSelected, T>(this IQuery<TSelected> before, T tbale) => InvalitContext.Throw<IQuery<TSelected>>(nameof(CrossJoin));

        public static IQuery<Non> Where(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Where));
        public static IQuery<TSelected> Where<TSelected>(this IQuery<TSelected> before, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Where));

        public static IQuery<Non> GroupBy(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupBy));
        public static IQuery<TSelected> GroupBy<TSelected>(this IQuery<TSelected> before, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupBy));

        public static IQuery<Non> GroupByRollup(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByRollup));
        public static IQuery<TSelected> GroupByRollup<TSelected>(this IQuery<TSelected> before, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByRollup));

        public static IQuery<Non> GroupByWithRollup(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByRollup));
        public static IQuery<TSelected> GroupByWithRollup<TSelected>(this IQuery<TSelected> before, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByRollup));

        public static IQuery<Non> GroupByCube(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByCube));
        public static IQuery<TSelected> GroupByCube<TSelected>(this IQuery<TSelected> before, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByCube));

        public static IQuery<Non> GroupByGroupingSets(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupByGroupingSets));
        public static IQuery<TSelected> GroupByGroupingSets<TSelected>(this IQuery<TSelected> before, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupByGroupingSets));

        public static IQuery<Non> Having(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Having));
        public static IQuery<TSelected> Having<TSelected>(this IQuery<TSelected> before, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Having));

        public static IQuery<Non> OrderBy(params ISortedBy[] elements) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> OrderBy<TSelected>(this IQuery<TSelected> before, params ISortedBy[] elements) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static IQuery<Non> Limit(long count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> Limit<TSelected>(this IQuery<TSelected> before, long count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static IQuery<Non> Limit(long offset, long count) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> Limit<TSelected>(this IQuery<TSelected> before, long offset, long count) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static IQuery<Non> Offset(long offset) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> Offset<TSelected>(this IQuery<TSelected> before, long offset) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static IQuery<Non> OffsetRows(long rowCount) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> OffsetRows<TSelected>(this IQuery<TSelected> before, long rowCount) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static IQuery<Non> FetchNextRowsOnly(long rowCount) => InvalitContext.Throw<IQuery<Non>>(nameof(OrderBy));
        public static IQuery<TSelected> FetchNextRowsOnly<TSelected>(this IQuery<TSelected> before, long rowCount) => InvalitContext.Throw<IQuery<TSelected>>(nameof(OrderBy));

        public static bool Like(string target, string serachText) => InvalitContext.Throw<bool>(nameof(Like));
        public static bool Between<TTarget>(TTarget target, TTarget min, TTarget max) => InvalitContext.Throw<bool>(nameof(Between));
        public static bool In<TTarget>(TTarget target, params TTarget[] inArguments) => InvalitContext.Throw<bool>(nameof(In));
        public static bool In<TTarget>(TTarget target, ISqlExpressionBase exp) => InvalitContext.Throw<bool>(nameof(In));
        public static bool In<TTarget>(TTarget target, IQuery exp) => InvalitContext.Throw<bool>(nameof(In));
        public static bool Exists(ISqlExpressionBase exp) => InvalitContext.Throw<bool>(nameof(Exists));
        public static bool Exists(IQuery exp) => InvalitContext.Throw<bool>(nameof(Exists));

        public static IQuery<Non> Union() => InvalitContext.Throw<IQuery<Non>>(nameof(Union));
        public static IQuery<TSelected> Union<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Union));

        public static IQuery<Non> Union(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Union));
        public static IQuery<TSelected> Union<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Union));

        public static IQuery<Non> Intersect() => InvalitContext.Throw<IQuery<Non>>(nameof(Intersect));
        public static IQuery<TSelected> Intersect<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Intersect));

        public static IQuery<Non> Intersect(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Intersect));
        public static IQuery<TSelected> Intersect<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Intersect));

        public static IQuery<Non> Except() => InvalitContext.Throw<IQuery<Non>>(nameof(Except));
        public static IQuery<TSelected> Except<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Except));

        public static IQuery<Non> Except(bool isAll) => InvalitContext.Throw<IQuery<Non>>(nameof(Except));
        public static IQuery<TSelected> Except<TSelected>(this IQuery<TSelected> before, bool isAll) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Except));

        public static IQuery<Non> Minus() => InvalitContext.Throw<IQuery<Non>>(nameof(Minus));
        public static IQuery<TSelected> Minus<TSelected>(this IQuery<TSelected> before) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Minus));

        [MethodGroup(nameof(Update))]
        public static IQuery<Non, T> Update<T>(T table) => InvalitContext.Throw<IQuery<Non, T>>(nameof(Update));
        [MethodGroup(nameof(Update))]
        public static IQuery<TSelected> Set<TSelected, T>(this IQuery<TSelected, T> before, params Assign[] assigns) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Set));

        public static IQuery<Non> Delete() => InvalitContext.Throw<IQuery<Non>>(nameof(Delete));

        [MethodGroup(nameof(InsertInto))]
        public static IQuery<Non, TTable> InsertInto<TTable>(TTable table, params object[] targets) => InvalitContext.Throw<IQuery<Non, TTable>>(nameof(InsertInto));

        [MethodGroup(nameof(InsertInto))]
        public static IQuery<TSelected> Values<TSelected, TTable>(this IQuery<TSelected, TTable> before, params object[] targets)
             => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));

        public static bool IsNull<T>(T target) => InvalitContext.Throw<bool>(nameof(IsNull));
        public static bool IsNotNull<T>(T target) => InvalitContext.Throw<bool>(nameof(IsNotNull));

        public static long RowNum => InvalitContext.Throw<long>(nameof(RowNum));

        public static object Dual => InvalitContext.Throw<object>(nameof(Dual));

        public static SysibmType Sysibm => InvalitContext.Throw<SysibmType>(nameof(Sysibm));

        static string ToString(ISqlStringConverter converter, MemberExpression member)
            => member.Member.Name.ToUpper();

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Select):
                    return SelectClause.ToString(converter, methods);
                case nameof(Case):
                    return CaseClause.ToString(converter, methods);
                case nameof(Delete):
                    return DeleteClause.ToString(converter, methods);
                case nameof(From):
                case nameof(Join):
                case nameof(LeftJoin):
                case nameof(RightJoin):
                case nameof(CrossJoin):
                    return FromClause.ToString(converter, methods);
                case nameof(GroupBy):
                case nameof(GroupByRollup):
                case nameof(GroupByWithRollup):
                case nameof(GroupByCube):
                case nameof(GroupByGroupingSets):
                    return GroupByClause.ToString(converter, methods);
                case nameof(Having):
                    return HavingClause.ToString(converter, methods);
                case nameof(InsertInto):
                    return InsertIntoClause.ToString(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsClause.ToString(converter, methods);
                case nameof(OffsetRows):
                    return OffsetRowsClause.ToString(converter, methods);
                case nameof(FetchNextRowsOnly):
                    return FetchNextRowsOnlyClause.ToString(converter, methods);
                case nameof(Limit):
                    return LimitClause.ToString(converter, methods);
                case nameof(Offset):
                    return OffsetClause.ToString(converter, methods);
                case nameof(Update):
                    return UpdateClause.ToString(converter, methods);
                case nameof(Where):
                    return WhereClause.ToString(converter, methods);
                case nameof(In):
                case nameof(Like):
                case nameof(Between):
                case nameof(Exists):
                    return ConditionKeyWords.ToString(converter, methods);
                case nameof(Union):
                case nameof(Intersect):
                case nameof(Except):
                case nameof(Minus):
                    return SetOperation.ToString(converter, methods);
                case nameof(IsNull):
                case nameof(IsNotNull):
                    return NullCheck.ToString(converter, methods);
                case nameof(Top):
                    return LambdicSql.Top.ToString(converter, methods);
                case nameof(Asterisk):
                    return LambdicSql.Asterisk.ToString(converter, methods);
                case nameof(Asc):
                    return LambdicSql.Asc.ToString(converter, methods);
                case nameof(Desc):
                    return LambdicSql.Desc.ToString(converter, methods);
            }
            throw new NotSupportedException();
        }
    }
}
