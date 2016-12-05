using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlExpressionBase
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);
    }

    public interface ISqlExpressionBase<out TReturn> : ISqlExpressionBase { }
}
