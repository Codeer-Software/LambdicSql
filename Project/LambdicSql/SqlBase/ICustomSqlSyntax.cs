using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ICustomSqlSyntax
    {
        string ToString(ISqlStringConverter converter, MethodCallExpression[] methods);
        string ToString(ISqlStringConverter converter, NewExpression exp);
    }
}
