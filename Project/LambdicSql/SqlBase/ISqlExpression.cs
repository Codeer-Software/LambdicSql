namespace LambdicSql.SqlBase
{
    public interface ISqlExpression
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }
    
    public interface ISqlExpression<out TReturn> : ISqlExpression { }
}
