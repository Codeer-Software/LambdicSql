using System;

namespace LambdicSql
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
