namespace LambdicSql.QueryBase
{
    public interface ISqlGroupingKeyWord : ISqlKeyWord, ISqlGroupingSyntax { }
    public interface ISqlGroupingKeyWord<TSelected> : ISqlKeyWord<TSelected>, ISqlGroupingKeyWord { }
}
