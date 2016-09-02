using LambdicSql.SqlBase;

namespace LambdicSql
{
    public interface ISqlExpression<out TReturn> : ISqlExpressionBase<TReturn>
    {
        TReturn Body { get; }
    }
}
