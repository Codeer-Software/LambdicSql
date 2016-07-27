using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Update
{
    public interface ISetQuery<TDB, TTable> : IQuery<TDB, TDB, UpdateClause>
            where TDB : class
            where TTable : class
    { }
}
