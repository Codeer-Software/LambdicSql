namespace LambdicSql.SqlBase
{
    public interface IMethodChain { }
    public interface IMethodChain<out T> : IMethodChain { }
}
