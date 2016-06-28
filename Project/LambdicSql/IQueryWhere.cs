namespace LambdicSql
{
    public interface IQueryWhere<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
