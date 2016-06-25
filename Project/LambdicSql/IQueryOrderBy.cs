namespace LambdicSql
{
    public interface IOrderByQuery<TDB, TSelect> : IQueryGroupByEnd<TDB, TSelect> 
        where TDB : class
        where TSelect : class
    { }
}
