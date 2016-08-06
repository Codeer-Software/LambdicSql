using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB> where TDB : class, new()
    {
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpressionSingle<TResult>(db, exp.Body);
        }
    }

    [SqlSyntax]
    public static class Sql
    {
        public static ISqlKeyWord<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Select));
        public static ISqlKeyWord<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Select));
        public static ISqlKeyWord<TSelected> SelectFrom<TSelected>(TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(SelectFrom));
        public static ISqlKeyWord<TSelected> Select<TSelected>(this ISqlKeyWord words, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Select));
        public static ISqlKeyWord<TSelected> Select<TSelected>(this ISqlKeyWord words, TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Select));
        public static ISqlKeyWord<TSelected> SelectFrom<TSelected>(this ISqlKeyWord words, TSelected selected) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(SelectFrom));

        public interface ICaseAfter : ISqlChainingKeyWord { }
        public interface IWhenAfter : ISqlChainingKeyWord { }
        public interface IWhenAfter<T> : ISqlChainingKeyWord { }
        public interface IThenAfter<T> : ISqlChainingKeyWord { }
        public interface IElseAfter<T> : ISqlChainingKeyWord { }
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static ICaseAfter Case<T>(T t) => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IWhenAfter<T1> When<T1, T2>(this IThenAfter<T1> words, T2 t) => InvalitContext.Throw<IWhenAfter<T1>>(nameof(When));
        public static IThenAfter<T> Then<T>(this IWhenAfter words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IElseAfter<T> Else<T>(this IThenAfter<T> words, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));
        public static T End<T>(this IThenAfter<T> words) => InvalitContext.Throw<T>(nameof(End));
        public static T End<T>(this IElseAfter<T> words) => InvalitContext.Throw<T>(nameof(End));

        public static ISqlKeyWord<Non> Delete() => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Delete));

        public interface IFromAfter<T> : ISqlChainingKeyWord<T> { }
        public static IFromAfter<TSelected> From<TSelected, T>(this ISqlKeyWord<TSelected> words, T tbale) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(From));
        public static ISqlKeyWord<TSelected> Join<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Join));
        public static ISqlKeyWord<TSelected> LeftJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(LeftJoin));
        public static ISqlKeyWord<TSelected> RightJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(RightJoin));
        public static ISqlKeyWord<TSelected> CrossJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(CrossJoin));
        public static IFromAfter<Non> From<T>(T tbale) => InvalitContext.Throw<IFromAfter<Non>>(nameof(From));

        public static ISqlKeyWord<Non> GroupBy(params object[] target) => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(GroupBy));
        public static ISqlKeyWord<TSelected> GroupBy<TSelected>(this ISqlKeyWord<TSelected> words, params object[] target) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(GroupBy));

        public static ISqlKeyWord<Non> Having(bool condition) => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Having));
        public static ISqlKeyWord<TSelected> Having<TSelected>(this ISqlKeyWord<TSelected> words, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Having));

        public interface IInsertIntoAfter<T> : ISqlChainingKeyWord<T> { }
        public static IInsertIntoAfter<Non> InsertInto<TTable>(TTable table, params object[] targets) => InvalitContext.Throw<IInsertIntoAfter<Non>>(nameof(InsertInto));
        public static ISqlKeyWord<TSelected> Values<TSelected>(this IInsertIntoAfter<TSelected> words, params object[] targets)
             => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Values));

        public interface IOrderByAfter<T> : ISqlChainingKeyWord<T> { }
        public static IOrderByAfter<Non> OrderBy() => InvalitContext.Throw<IOrderByAfter<Non>>(nameof(OrderBy));
        public static IOrderByAfter<TSelected> OrderBy<TSelected>(this ISqlKeyWord<TSelected> words) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(OrderBy));
        public static IOrderByAfter<TSelected> ASC<TSelected, T>(this IOrderByAfter<TSelected> words, T target) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(ASC));
        public static IOrderByAfter<TSelected> DESC<TSelected, T>(this IOrderByAfter<TSelected> words, T target) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(DESC));

        public interface IUpdateAfter<TSelected, T> : ISqlChainingKeyWord<TSelected> { }
        public static IUpdateAfter<Non, T> Update<T>(T table)  => InvalitContext.Throw<IUpdateAfter<Non, T>>(nameof(Update));
        public static ISqlKeyWord<TSelected> Set<TSelected, T>(this IUpdateAfter<TSelected, T> words, T setting) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Set));

        public static ISqlKeyWord<Non> Where(bool condition) => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Where));
        public static ISqlKeyWord<TSelected> Where<TSelected>(this ISqlKeyWord<TSelected> words, bool condition) => InvalitContext.Throw<ISqlKeyWord<TSelected>>(nameof(Where));

        public static bool Like(string target, string serachText) => InvalitContext.Throw<bool>(nameof(Like));
        public static bool Between<TTarget>(TTarget target, TTarget min, TTarget max) => InvalitContext.Throw<bool>(nameof(Between));
        public static bool In<TTarget>(TTarget target, params TTarget[] inArguments) => InvalitContext.Throw<bool>(nameof(In));

        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Select):
                case nameof(SelectFrom):
                    return SelectClause.MethodsToString(converter, methods);
                case nameof(Case):
                    return CaseClause.MethodsToString(converter, methods);
                case nameof(Delete):
                    return DeleteClause.MethodsToString(converter, methods);
                case nameof(From):
                    return FromClause.MethodsToString(converter, methods);
                case nameof(GroupBy):
                    return GroupByClause.MethodsToString(converter, methods);
                case nameof(Having):
                    return HavingClause.MethodsToString(converter, methods);
                case nameof(InsertInto):
                    return InsertIntoClause.MethodsToString(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsClause.MethodsToString(converter, methods);
                case nameof(Update):
                    return UpdateClause.MethodsToString(converter, methods);
                case nameof(Where):
                    return WhereClause.MethodsToString(converter, methods);
                case nameof(In):
                case nameof(Like):
                case nameof(Between):
                    return ConditionKeyWords.MethodsToString(converter, methods);
            }
            throw new NotSupportedException();
        }
    }
}
