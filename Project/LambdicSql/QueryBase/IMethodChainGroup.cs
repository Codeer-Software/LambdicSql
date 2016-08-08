namespace LambdicSql.QueryBase
{
    public interface IMethodChainGroup : IMethodChain { }
    public interface IMethodChainGroup<T> : IMethodChain<T>, IMethodChainGroup { }
}
