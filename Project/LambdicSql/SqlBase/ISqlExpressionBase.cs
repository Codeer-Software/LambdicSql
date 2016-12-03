using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlExpressionBase
    {
        DbInfo DbInfo { get; }
        string ToString(ISqlStringConverter decoder);

        //TODO ない方がいい
        Expression[] GetExpressions();
    }

    public interface ISqlExpressionBase<out TReturn> : ISqlExpressionBase { }
}
