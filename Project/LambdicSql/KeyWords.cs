using LambdicSql.Inside;
using LambdicSql.Inside.Keywords;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    [SqlSyntax]
    public static class Keywords
    {
        public static IQuery<TSelected> Select<TSelected>(AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> Select<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> SelectFrom<TSelected>(TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(SelectFrom));
        public static IQuery<TSelected> Select<TSelected>(this IQuery words, AggregatePredicate predicate, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> Select<TSelected>(this IQuery words, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Select));
        public static IQuery<TSelected> SelectFrom<TSelected>(this IQuery words, TSelected selected) => InvalitContext.Throw<IQuery<TSelected>>(nameof(SelectFrom));

        public interface ICaseAfter : IQueryGroup { }
        public interface IWhenAfter : IQueryGroup { }
        public interface IWhenAfter<T> : IQueryGroup { }
        public interface IThenAfter<T> : IQueryGroup { }
        public interface IElseAfter<T> : IQueryGroup { }
        public static ICaseAfter Case() => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static ICaseAfter Case<T>(T t) => InvalitContext.Throw<ICaseAfter>(nameof(Case));
        public static IWhenAfter When<T>(this ICaseAfter words, T t) => InvalitContext.Throw<IWhenAfter>(nameof(When));
        public static IWhenAfter<T1> When<T1, T2>(this IThenAfter<T1> words, T2 t) => InvalitContext.Throw<IWhenAfter<T1>>(nameof(When));
        public static IThenAfter<T> Then<T>(this IWhenAfter words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IThenAfter<T> Then<T>(this IWhenAfter<T> words, T t) => InvalitContext.Throw<IThenAfter<T>>(nameof(Then));
        public static IElseAfter<T> Else<T>(this IThenAfter<T> words, T t) => InvalitContext.Throw<IElseAfter<T>>(nameof(Then));
        public static T End<T>(this IThenAfter<T> words) => InvalitContext.Throw<T>(nameof(End));
        public static T End<T>(this IElseAfter<T> words) => InvalitContext.Throw<T>(nameof(End));

        public static IQuery<Non> Delete() => InvalitContext.Throw<IQuery<Non>>(nameof(Delete));

        public interface IFromAfter<T> : IQueryGroup<T> { }
        public static IFromAfter<TSelected> From<TSelected, T>(this IQuery<TSelected> words, T tbale) => InvalitContext.Throw<IFromAfter<TSelected>>(nameof(From));
        public static IQuery<TSelected> Join<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Join));
        public static IQuery<TSelected> LeftJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(LeftJoin));
        public static IQuery<TSelected> RightJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(RightJoin));
        public static IQuery<TSelected> CrossJoin<TSelected, T>(this IFromAfter<TSelected> words, T tbale) => InvalitContext.Throw<IQuery<TSelected>>(nameof(CrossJoin));
        public static IFromAfter<Non> From<T>(T tbale) => InvalitContext.Throw<IFromAfter<Non>>(nameof(From));

        public static IQuery<Non> GroupBy(params object[] target) => InvalitContext.Throw<IQuery<Non>>(nameof(GroupBy));
        public static IQuery<TSelected> GroupBy<TSelected>(this IQuery<TSelected> words, params object[] target) => InvalitContext.Throw<IQuery<TSelected>>(nameof(GroupBy));

        public static IQuery<Non> Having(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Having));
        public static IQuery<TSelected> Having<TSelected>(this IQuery<TSelected> words, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Having));

        public interface IInsertIntoAfter<T> : IQueryGroup<T> { }
        public static IInsertIntoAfter<Non> InsertInto<TTable>(TTable table, params object[] targets) => InvalitContext.Throw<IInsertIntoAfter<Non>>(nameof(InsertInto));
        public static IQuery<TSelected> Values<TSelected>(this IInsertIntoAfter<TSelected> words, params object[] targets)
             => InvalitContext.Throw<IQuery<TSelected>>(nameof(Values));

        public interface IOrderByAfter<T> : IQueryGroup<T> { }
        public static IOrderByAfter<Non> OrderBy(params IOrderElement[] elements) => InvalitContext.Throw<IOrderByAfter<Non>>(nameof(OrderBy));
        public static IOrderByAfter<TSelected> OrderBy<TSelected>(this IQuery<TSelected> words, params IOrderElement[] elements) => InvalitContext.Throw<IOrderByAfter<TSelected>>(nameof(OrderBy));

        public interface IUpdateAfter<TSelected, T> : IQueryGroup<TSelected> { }
        public static IUpdateAfter<Non, T> Update<T>(T table) => InvalitContext.Throw<IUpdateAfter<Non, T>>(nameof(Update));
        public static IQuery<TSelected> Set<TSelected, T>(this IUpdateAfter<TSelected, T> words, T setting) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Set));

        public static IQuery<Non> Where(bool condition) => InvalitContext.Throw<IQuery<Non>>(nameof(Where));
        public static IQuery<TSelected> Where<TSelected>(this IQuery<TSelected> words, bool condition) => InvalitContext.Throw<IQuery<TSelected>>(nameof(Where));

        public static bool Like(string target, string serachText) => InvalitContext.Throw<bool>(nameof(Like));
        public static bool Between<TTarget>(TTarget target, TTarget min, TTarget max) => InvalitContext.Throw<bool>(nameof(Between));
        public static bool In<TTarget>(TTarget target, params TTarget[] inArguments) => InvalitContext.Throw<bool>(nameof(In));

        static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
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
