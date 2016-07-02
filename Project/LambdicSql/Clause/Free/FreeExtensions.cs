using LambdicSql.Clause.Free;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class FreeExtensions
    {
        public static IQuery<TDB, TSelect, FreeClause> Free<TDB, TSelect>(this IQuery<TDB, TSelect> query, string text)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, FreeClause>(query, new FreeClause(text));
    }
}
