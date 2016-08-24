namespace LambdicSql.SqlBase
{
    public interface IQueryGroup : IQuery, IMethodChainGroup { }
    public interface IQueryGroup<out TSelected> : IQuery<TSelected>, IQueryGroup { }
}
