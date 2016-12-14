using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SetOperation
    {
        internal static TextParts ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var text = method.Method.Name.ToUpper();
            var index = method.SkipMethodChain(0);
            if (index < method.Arguments.Count)
            {
                var obj = converter.ToObject(method.Arguments[index]);
                if ((bool)obj)
                {
                    text += " ALL";
                }
            }
            return new SingleText(text);
        }
    }
}
