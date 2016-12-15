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
        /// Expression to text.
        /// </summary>
        /// <param name="convertor">convertor.</param>
        /// <returns>text.</returns>
        SqlText Convert(ISqlStringConverter convertor);
    }

    /// <summary>
    /// Expression.
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface ISqlExpressionBase<out TReturn> : ISqlExpressionBase { }
}
