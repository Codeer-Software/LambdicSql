namespace LambdicSql.SqlBase
{
    public interface IQuery : IMethodChain { }
    public interface IQuery<TSelected> : IQuery, IMethodChain<TSelected> { }
}
