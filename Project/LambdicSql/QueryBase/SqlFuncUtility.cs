using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.QueryBase
{
    public static class SqlFuncUtility
    {
        public static string MakeNormalSqlFunctionString(this ISqlStringConverter convertor, MethodCallExpression method)
            => method.Method.Name + "(" + string.Join(", ", method.Arguments.Skip(1).Select(e => convertor.ToString(e)).ToArray()) + ")";      
    }
}
