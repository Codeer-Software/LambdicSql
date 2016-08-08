namespace LambdicSql.QueryBase
{
    public interface IQuery : IMethodChain { }
    public interface IQuery<TSelected> : IQuery, IMethodChain<TSelected> { }
}
