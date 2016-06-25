namespace LambdicSql
{
    public interface IQueryGroupByEnd<TDB, TSelect> : IQueryOrderByEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
