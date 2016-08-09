using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.SqlBase
{
    public static class SqlFuncUtility
    {
        public static string MakeNormalSqlFunctionString(this ISqlStringConverter convertor, MethodCallExpression method)
            => method.Method.Name.ToUpper() + "(" + string.Join(", ", method.Arguments.Skip(method.AdjustSqlSyntaxMethodArgumentIndex(0)).Select(e => convertor.ToString(e)).ToArray()) + ")";      
    }
}
