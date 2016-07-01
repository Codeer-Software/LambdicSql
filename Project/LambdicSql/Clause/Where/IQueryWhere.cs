using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Where
{
    public interface IQueryWhere<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
