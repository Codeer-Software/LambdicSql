using LambdicSql.Clause.Delete;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class DeleteExtensions
    {
        public static IQuery<TDB, TSelect, DeleteClause> Delete<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, DeleteClause>(query, new DeleteClause());
    }
}
