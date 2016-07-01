using LambdicSql.Inside;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public static class QueryExtensions
    {
        public static TSelect ToSubQuery<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class 
            => default(TSelect);

        public static IDBExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbAdapter adaptor)
             where TDB : class
             where TSelect : class
        {
            return new SqlExecutor<TSelect>(adaptor, query as IQuery<TDB, TSelect>);
        }
    }
}

//TODO sub query in From clauses

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
