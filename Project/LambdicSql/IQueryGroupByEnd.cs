namespace LambdicSql
{
    public interface IQueryGroupByEnd<TDB, TSelect> : IQueryHavingEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
