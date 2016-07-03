using LambdicSql.Clause.Update;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class UpdateExtensions
    {
        public static IQuery<TDB, TSelect, UpdateClause> Delete<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, UpdateClause>(query, new UpdateClause());

    }
}
