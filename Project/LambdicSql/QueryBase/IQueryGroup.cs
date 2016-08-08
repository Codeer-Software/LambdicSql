namespace LambdicSql.QueryBase
{
    public interface IQueryGroup : IQuery, IMethodChainGroup { }
    public interface IQueryGroup<TSelected> : IQuery<TSelected>, IQueryGroup { }
}
