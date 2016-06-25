namespace LambdicSql
{
    public interface IWhereQuery<TDB, TSelect> : IQueryFromEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }

    public interface IWhereQueryNot<TDB, TSelect> : IQueryFromEnd<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }

    public interface IWhereQueryConnectable<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }

    public interface IWhereQueryConnectableNot<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }
}
