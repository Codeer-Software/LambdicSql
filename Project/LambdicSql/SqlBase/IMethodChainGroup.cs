namespace LambdicSql.SqlBase
{
    //TODO ★これいらん
    public interface IMethodChainGroup : IMethodChain { }
    public interface IMethodChainGroup<T> : IMethodChain<T>, IMethodChainGroup { }
}
