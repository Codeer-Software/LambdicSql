namespace LambdicSql.SqlBase
{
    //TODO name is clause?
    public interface IQuery : IMethodChain { }
    public interface IQuery<out TSelected> : IQuery, IMethodChain<TSelected> { }
    public interface IQuery<out TSelected, out TInfo> : IQuery<TSelected> { }
}
