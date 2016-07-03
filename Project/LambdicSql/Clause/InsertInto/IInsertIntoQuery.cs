using LambdicSql.QueryBase;

namespace LambdicSql.Clause.InsertInto
{
    public interface IInsertIntoQuery<TDB, TTable> : IQuery<TDB, TDB, InsertIntoClause<TDB, TTable>>
            where TDB : class
            where TTable : class
    {
        TTable GetTable(TDB db);
    }
}
