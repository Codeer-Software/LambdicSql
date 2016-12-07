using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Interfaces representing expressions in LambdicSql.
    /// </summary>
    /// <typeparam name="TReturn">Expression return type.</typeparam>
    public interface ISqlExpression<out TReturn> : ISqlExpressionBase<TReturn>
    {
        /// <summary>
        /// The type that the expression represents.
        /// </summary>
        TReturn Body { get; }
    }
}
