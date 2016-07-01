using System;

namespace LambdicSql.QueryBase
{
    public interface IQuery
    {
        DbInfo Db { get; }
        IClause[] GetClausesClone();
    }

    public interface IQuery<TSelect> : IQuery
        where TSelect : class
    {
        Func<IDbResult, TSelect> Create { get; }
    }

    public interface IQuery<TDB, TSelect> : IQuery<TSelect>
        where TDB : class
        where TSelect : class
    {
    }

    public interface IQuery<TDB, TSelect, TClause> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
        where TClause : IClause
    {
        IQuery<TDB, TSelect, TClause> CustomClone(Action<TClause> custom);
    }
}

