using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.ExpressionElements;
using LambdicSql.SqlBuilder.ExpressionElements.Inside;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class CustomizeColumnOnly : ISqlTextCustomizer
    {
        public ExpressionElement Custom(ExpressionElement src)
        {
            var col = src as DbColumnText;
            if (col == null) return src;
            return col.ToColumnOnly();
        }
    }
}
