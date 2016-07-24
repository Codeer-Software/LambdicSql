using System;

namespace LambdicSql.QueryBase
{
    public interface IQuery
    {
        DbInfo Db { get; }
        IClause[] GetClausesClone();
    }

    public interface ISelectedQuery<TSelect> : IQuery
        where TSelect : class
    {
        Func<ISqlResult, TSelect> Create { get; }
    }

    public interface IQuery<TDB> : IQuery
        where TDB : class
    { }

    public interface IQuery<TDB, TSelect> : IQuery<TDB>, ISelectedQuery<TSelect>
        where TDB : class
        where TSelect : class
    { }

    public interface IQuery<TDB, TSelect, TClause> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
        where TClause : IClause
    {
        IQuery<TDB, TSelect, TClause> CustomClone(Action<TClause> custom);
    }
}
