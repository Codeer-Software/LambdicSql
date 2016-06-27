namespace LambdicSql
{
    public interface IQueryHavingEnd<TDB, TSelect> : IQueryOrderByEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
