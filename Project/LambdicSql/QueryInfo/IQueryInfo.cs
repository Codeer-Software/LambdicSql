using System;

namespace LambdicSql.QueryInfo
{
    //@@@ info to List<object>
    public interface IQueryInfo
    {
        DbInfo Db { get; }
        SelectClause Select { get; }
        FromClause From { get; }
        ConditionClause Where { get; }
        GroupByClause GroupBy { get; }
        ConditionClause Having { get; }
        OrderByClause OrderBy { get; }
    }

    public interface IQueryInfo<TSelect> : IQueryInfo
    {
        Func<IDbResult, TSelect> Create { get; }
    }
}
