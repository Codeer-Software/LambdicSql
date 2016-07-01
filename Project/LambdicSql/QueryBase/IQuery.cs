namespace LambdicSql.QueryBase
{
    public interface IQuery { }

    public interface IQuery<TDB, TSelect> : IQuery
        where TDB : class
        where TSelect : class
    { }
}

