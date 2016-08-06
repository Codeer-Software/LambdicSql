namespace LambdicSql.QueryBase
{
    public interface ISqlExpression
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }

    //TODO implecit cast...
    public interface ISqlExpression<out TSelected> : ISqlExpression { }
}
