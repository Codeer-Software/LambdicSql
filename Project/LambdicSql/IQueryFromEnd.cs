namespace LambdicSql
{
    public interface IQueryFromEnd<TDB, TSelect> : IQueryWhereEnd<TDB, TSelect>
       where TDB : class
        where TSelect : class
    { }
}
