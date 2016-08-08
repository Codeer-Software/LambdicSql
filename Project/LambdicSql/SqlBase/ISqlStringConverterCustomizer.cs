using System;
using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlStringConverterCustomizer
    {
        string CustomOperator(Type lhs, string @operator, Type rhs);
        string CusotmMethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods);
    }
}
