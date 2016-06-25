namespace LambdicSql
{
    public interface IQueryWhereEnd<TDB, TSelect> : IQueryGroupByEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
