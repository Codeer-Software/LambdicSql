using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxColumnOnlyAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var col = converter.Convert(expression.Arguments[0]) as DbColumnParts;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
