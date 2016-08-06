using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public interface ISqlExpression
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }

    public interface ISqlExpression<out TSelected> : ISqlExpression { }
}
