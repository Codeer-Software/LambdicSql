using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SetOperation
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var obj = converter.ToObject(method.Arguments[method.AdjustSqlSyntaxMethodArgumentIndex(0)]);
            var text = method.Method.Name.ToUpper();
            if ((bool)obj)
            {
                text += " ALL";
            }
            return text;
        }
    }
}
