using System;

namespace LambdicSql.QueryInfo
{
    //@@@ info to List<object>
    public interface IQueryInfo
    {
        DbInfo Db { get; }
        SelectInfo Select { get; }
        FromInfo From { get; }
        ConditionClauseInfo Where { get; }
        GroupByInfo GroupBy { get; }
        ConditionClauseInfo Having { get; }
        OrderByInfo OrderBy { get; }
    }

    public interface IQueryInfo<TSelect> : IQueryInfo
    {
        Func<IDbResult, TSelect> Create { get; }
    }
}
