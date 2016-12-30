using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
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
