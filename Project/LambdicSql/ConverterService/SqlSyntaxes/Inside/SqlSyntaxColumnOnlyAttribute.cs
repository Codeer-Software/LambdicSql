using LambdicSql.SqlBuilder.Parts;
using LambdicSql.SqlBuilder.Parts.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxColumnOnlyAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
