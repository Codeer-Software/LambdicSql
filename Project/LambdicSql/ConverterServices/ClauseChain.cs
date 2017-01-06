using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Clause chain.
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public abstract class ClauseChain<TSelected> : IMethodChain
    {
        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator TSelected(ClauseChain<TSelected> src) => InvalitContext.Throw<TSelected>("implicit operator");
    }
}
