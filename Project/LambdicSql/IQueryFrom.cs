namespace LambdicSql
{
    public interface IQueryFrom<TDB, TSelect> : IQueryFromEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
