namespace LambdicSql
{
    public interface IQueryHaving<TDB, TSelect> : IQueryHavingEnd<TDB, TSelect>
       where TDB : class
       where TSelect : class
    { }
}
