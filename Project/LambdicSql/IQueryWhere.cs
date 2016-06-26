namespace LambdicSql
{
    public interface IWhereQuery<TDB, TSelect> : IQueryWhereEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
