namespace LambdicSql
{
    public interface IQueryOrderBy<TDB, TSelect> : IQueryGroupByEnd<TDB, TSelect> 
        where TDB : class
        where TSelect : class
    { }
}
