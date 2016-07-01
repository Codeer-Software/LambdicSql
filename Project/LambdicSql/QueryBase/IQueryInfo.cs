using LambdicSql.Clause.From;
using LambdicSql.Clause.GroupBy;
using LambdicSql.Clause.Having;
using LambdicSql.Clause.OrderBy;
using LambdicSql.Clause.Select;
using LambdicSql.Clause.Where;
using System;

namespace LambdicSql.QueryBase
{
    //@@@ info to List<object>
    public interface IQueryInfo
    {
        DbInfo Db { get; }
        SelectClause Select { get; }
        FromClause From { get; }
        WhereClause Where { get; }
        GroupByClause GroupBy { get; }
        HavingClause Having { get; }
        OrderByClause OrderBy { get; }
    }

    public interface IQueryInfo<TSelect> : IQueryInfo
    {
        Func<IDbResult, TSelect> Create { get; }
    }
}
