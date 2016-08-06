using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB> where TDB : class, new()
    {
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, ISqlKeyWord<Non>, TResult>> exp)
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
        public static CaseWordsExtensions.ICaseAfter Case() => InvalitContext.Throw<CaseWordsExtensions.ICaseAfter>(nameof(Case));
        public static CaseWordsExtensions.ICaseAfter Case<T>(T t) => InvalitContext.Throw<CaseWordsExtensions.ICaseAfter>(nameof(Case));
        public static ISqlKeyWord<Non> Delete() => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Delete));
        public static FromWordsExtensions.IFromAfter<Non> From<T>(T tbale) 
            => InvalitContext.Throw<FromWordsExtensions.IFromAfter<Non>>(nameof(From));
        public static ISqlKeyWord<Non> GroupBy(params object[] target) => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(GroupBy));
        public static ISqlKeyWord<Non> Having(bool condition) => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Having));
        public static InsertIntoWordsExtensions.IInsertIntoAfter<Non> InsertInto<TTable>(TTable table, params object[] targets)
             => InvalitContext.Throw<InsertIntoWordsExtensions.IInsertIntoAfter<Non>>(nameof(InsertInto));
        public static OrderByWordsExtensions.IOrderByAfter<Non> OrderBy()
            => InvalitContext.Throw<OrderByWordsExtensions.IOrderByAfter<Non>>(nameof(OrderBy));
        public static UpdateWordsExtensions.IUpdateAfter<Non, T> Update<T>(T table) 
            => InvalitContext.Throw<UpdateWordsExtensions.IUpdateAfter<Non, T>>(nameof(Update));
        public static ISqlKeyWord<Non> Where(bool condition) 
            => InvalitContext.Throw<ISqlKeyWord<Non>>(nameof(Where));

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
                    return SelectWordsExtensions.MethodsToString(converter, methods);
                case nameof(Case):
                    return CaseWordsExtensions.MethodsToString(converter, methods);
                case nameof(Delete):
                    return DeleteWordsExtensions.MethodsToString(converter, methods);
                case nameof(From):
                    return FromWordsExtensions.MethodsToString(converter, methods);
                case nameof(GroupBy):
                    return GroupByWordsExtensions.MethodsToString(converter, methods);
                case nameof(Having):
                    return HavingWordsExtensions.MethodsToString(converter, methods);
                case nameof(InsertInto):
                    return InsertIntoWordsExtensions.MethodsToString(converter, methods);
                case nameof(OrderBy):
                    return OrderByWordsExtensions.MethodsToString(converter, methods);
                case nameof(Update):
                    return UpdateWordsExtensions.MethodsToString(converter, methods);
                case nameof(Where):
                    return WhereWordsExtensions.MethodsToString(converter, methods);
                case nameof(In):
                case nameof(Like):
                case nameof(Between):
                    return WordsExtensions.MethodsToString(converter, methods);
            }
            throw new NotSupportedException();
        }
    }
}
