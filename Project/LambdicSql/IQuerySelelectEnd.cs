namespace LambdicSql
{
    public interface IQuerySelelectEnd<TDB, TSelect> : IQueryFromEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
