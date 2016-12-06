namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Method chain.
    /// </summary>
    public interface IMethodChain { }

    /// <summary>
    /// Method chain.
    /// </summary>
    /// <typeparam name="T">type of result.</typeparam>
    public interface IMethodChain<out T> : IMethodChain { }
}
