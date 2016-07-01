using LambdicSql.Inside;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class QueryExtensions
    {
        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static TSelect ToSubQuery<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class 
            => default(TSelect);

        public static IDbExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbAdapter adaptor)
             where TDB : class
             where TSelect : class
        {
            return new DbExecutor<TSelect>(adaptor, query as IQuery<TDB, TSelect>);
        }
    }
}

/*TODO clauses
Distinct
		new Distinct()
		new Distinct<int>(obj)

Case
UNION/EXCEPT/EXCEPT
Join many version. 
    INNER JOIN  → JOIN
    LEFT OUTER JOIN → LEFT JOIN
    RIGHT OUTER JOIN → RIGHT JOIN
    CROSS JOIN
*/
