using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Expression.SqlSyntax.Inside
{

    class SqlSyntaxLikeAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }
    }
}
