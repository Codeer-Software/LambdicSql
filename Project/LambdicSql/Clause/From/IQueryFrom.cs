using LambdicSql.QueryBase;

namespace LambdicSql.Clause.From
{
    public interface IQueryFrom<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
