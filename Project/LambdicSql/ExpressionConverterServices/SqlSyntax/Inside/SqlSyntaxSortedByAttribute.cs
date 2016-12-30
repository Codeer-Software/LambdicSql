using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax.Inside
{

    class SqlSyntaxSortedByAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => LineSpace(converter.Convert(method.Arguments[0]), method.Method.Name.ToUpper());
    }
}
