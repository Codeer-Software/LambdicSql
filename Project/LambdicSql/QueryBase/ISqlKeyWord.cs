namespace LambdicSql.QueryBase
{
    public interface ISqlKeyWord : ISqlSyntax { }
    public interface ISqlKeyWord<TSelected> : ISqlKeyWord { }

    public interface ISqlChainingSyntax : ISqlSyntax { }
    public interface ISqlChainingKeyWord : ISqlKeyWord, ISqlChainingSyntax { }
    public interface ISqlChainingKeyWord<TSelected> : ISqlKeyWord<TSelected>, ISqlChainingKeyWord { }
}
