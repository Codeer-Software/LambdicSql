using System;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public interface ISqlStringConverterCustomizer
    {
        string CustomOperator(Type lhs, string @operator, Type rhs);
        string CusotmMethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods);
    }
}
