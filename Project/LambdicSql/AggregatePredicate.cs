using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Aggregation predicate.
    /// </summary>
    [SqlSyntax]
    public enum AggregatePredicate
    {
        /// <summary>
        /// All.
        /// </summary>
        All,

        /// <summary>
        /// Distinct.
        /// </summary>
        Distinct
    }
}
