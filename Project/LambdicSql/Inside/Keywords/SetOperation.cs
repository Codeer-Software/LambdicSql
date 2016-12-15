using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SetOperation
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var clause = method.Method.Name.ToUpper();
            var index = method.SkipMethodChain(0);
            if (index < method.Arguments.Count)
            {
                var obj = converter.ToObject(method.Arguments[index]);
                if ((bool)obj) clause += " ALL";
            }
            return clause;
        }
    }
}
