using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Update
{
    public interface IUpdateQuery<TDB, TTable> : IQuery<TDB, TDB, UpdateClause>
            where TDB : class
            where TTable : class
    { }
}
