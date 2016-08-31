namespace LambdicSql.SqlBase
{
    public interface IQuery : IMethodChain { }
    public interface IQuery<out TSelected> : IQuery, IMethodChain<TSelected> { }
    public interface IQuery<out TSelected, out TInfo> : IQuery<TSelected> { }
}
