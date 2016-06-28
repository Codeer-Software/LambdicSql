namespace LambdicSql
{
    public interface IQueryGroupBy<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
