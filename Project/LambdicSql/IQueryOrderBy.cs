namespace LambdicSql
{
    public interface IQueryOrderBy<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
