namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Query.
    /// </summary>
    public interface IClauseChain : IMethodChain { }

    /// <summary>
    /// Query.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public interface IClauseChain<out TSelected> : IClauseChain, IMethodChain<TSelected>
    {
        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        TSelected Body { get; }
    }
}
