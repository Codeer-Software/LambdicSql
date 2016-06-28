namespace LambdicSql
{
    public interface IQueryFrom<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
