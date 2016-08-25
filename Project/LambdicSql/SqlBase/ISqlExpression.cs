namespace LambdicSql.SqlBase
{
    public interface ISqlExpression
    {
        object DbContext { get; }
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }
    
    public interface ISqlExpression<out T> : ISqlExpression { }
}
