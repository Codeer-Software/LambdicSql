namespace LambdicSql
{
    public interface IQueryHaving<TDB, TSelect> : IQuery<TDB, TSelect>
       where TDB : class
       where TSelect : class
    { }
}
