using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Interface representing queries in LambdicSql.
    /// </summary>
    public interface ISqlQuery : ISqlExpressionBase { }

    /// <summary>
    /// Interface representing queries in LambdicSql.
    /// </summary>
    /// <typeparam name="TSelected">The type specified in the Select clause.</typeparam>
    public interface ISqlQuery<out TSelected> : ISqlExpressionBase<IClauseChain<TSelected>>, ISqlQuery
    {
        /// <summary>
        /// It is the type selected in the SELECT clause.
        /// </summary>
        TSelected Body { get; }
    }
}

