using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Interfaces representing expressions in LambdicSql.
    /// </summary>
    /// <typeparam name="TReturn">Expression return type.</typeparam>
    public interface ISqlExpression<out TReturn> : ISqlExpressionBase<TReturn>
    {
        TReturn Body { get; }
    }
}
