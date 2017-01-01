using LambdicSql.SqlBuilder.ExpressionElements;
using LambdicSql.SqlBuilder.ExpressionElements.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxColumnOnlyAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
