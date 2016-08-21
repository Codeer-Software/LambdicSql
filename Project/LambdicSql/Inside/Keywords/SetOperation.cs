using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SetOperation
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)], out obj);
            var text = method.Method.Name.ToUpper();
            if ((bool)obj)
            {
                text += " ALL";
            }
            return text;
        }
    }
}
