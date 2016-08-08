namespace LambdicSql.SqlBase
{
    public interface IMethodChainGroup : IMethodChain { }
    public interface IMethodChainGroup<T> : IMethodChain<T>, IMethodChainGroup { }
}
