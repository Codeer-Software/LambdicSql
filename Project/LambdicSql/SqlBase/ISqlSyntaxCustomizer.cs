using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlSyntaxCustomizer
    {
        string ToString(ISqlStringConverter converter, object obj);
        string ToString(ISqlStringConverter converter, MethodCallExpression[] methods);
        string ToString(ISqlStringConverter converter, NewExpression exp);
    }
}
