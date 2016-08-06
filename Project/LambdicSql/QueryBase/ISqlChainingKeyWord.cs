namespace LambdicSql.QueryBase
{
    public interface ISqlChainingKeyWord : ISqlKeyWord, ISqlChainingSyntax { }
    public interface ISqlChainingKeyWord<TSelected> : ISqlKeyWord<TSelected>, ISqlChainingKeyWord { }
}
