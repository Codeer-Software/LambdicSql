namespace LambdicSql
{
    public interface IQueryStart<TDB, TSelect> : IQuerySelelectEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
