namespace LambdicSql
{
    public interface IQueryOrderByEnd<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
