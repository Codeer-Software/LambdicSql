using LambdicSql.QueryBase;

namespace LambdicSql.Clause.OrderBy
{
    public interface IQueryOrderBy<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
