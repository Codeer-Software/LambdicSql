using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Expression.
    /// </summary>
    public interface ISqlExpressionBase
    {
        /// <summary>
        /// Data Base info.
        /// </summary>
        DbInfo DbInfo { get; }

        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        SqlText SqlText { get; }
    }

    /// <summary>
    /// Expression.
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface ISqlExpressionBase<out TReturn> : ISqlExpressionBase { }
}
