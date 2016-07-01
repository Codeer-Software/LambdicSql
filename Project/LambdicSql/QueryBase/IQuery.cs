using LambdicSql.Clause.From;
using LambdicSql.Clause.GroupBy;
using LambdicSql.Clause.Having;
using LambdicSql.Clause.OrderBy;
using LambdicSql.Clause.Select;
using LambdicSql.Clause.Where;
using System;

namespace LambdicSql.QueryBase
{
    public interface IQuery
    {
        DbInfo Db { get; }
        SelectClause Select { get; }
        FromClause From { get; }
        WhereClause Where { get; }
        GroupByClause GroupBy { get; }
        HavingClause Having { get; }
        OrderByClause OrderBy { get; }
    }

    public interface IQuery<TSelect> : IQuery
        where TSelect : class
    {
        Func<IDbResult, TSelect> Create { get; }
    }

    public interface IQuery<TDB, TSelect> : IQuery<TSelect>
        where TDB : class
        where TSelect : class
    {
    }
}

