using LambdicSql.Inside;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Clause chain.
    /// </summary>
    public interface IClauseChain { }

    /// <summary>
    /// Clause chain.
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public class ClauseChain<TSelected> : IClauseChain
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        internal protected ClauseChain() { }

        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public TSelected Body => InvalitContext.Throw<TSelected>(nameof(Body));

        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator TSelected(ClauseChain<TSelected> src) => InvalitContext.Throw<TSelected>("implicit operator");
    }
}
