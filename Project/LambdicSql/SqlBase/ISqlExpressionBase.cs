using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlExpressionBase
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter convertor);
    }

    public interface ISqlExpressionBase<out TReturn> : ISqlExpressionBase { }
}
