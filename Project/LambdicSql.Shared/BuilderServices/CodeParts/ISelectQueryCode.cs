namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Code with select clause at the beginning. The select clause is special and needs to be distinguished from others.
    /// </summary>
    public interface ISelectQueryCode : ICode
    {
        /// <summary>
        /// Code.
        /// </summary>
        ICode Core { get; }

        /// <summary>
        /// Create ISelectQueryCode.
        /// </summary>
        /// <param name="core">Code.</param>
        /// <returns>ISelectQueryCode.</returns>
        ISelectQueryCode Create(ICode core);
    }
}
