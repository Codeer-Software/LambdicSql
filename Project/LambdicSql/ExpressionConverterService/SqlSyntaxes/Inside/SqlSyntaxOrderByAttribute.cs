using LambdicSql.ExpressionConverterService.Inside;
using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxOrderByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var arg = method.Arguments[method.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VText();
            orderBy.Add("ORDER BY");
            var sort = new VText() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }

}
