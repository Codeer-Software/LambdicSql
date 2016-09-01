namespace LambdicSql.SqlBase
{
    public interface ISqlExpression<out TReturn> : ISqlExpressionBase<TReturn>
    {
        TReturn Body { get; }
    }
}
