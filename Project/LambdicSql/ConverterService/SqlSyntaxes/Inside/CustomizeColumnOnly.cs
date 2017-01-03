using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Parts;
using LambdicSql.SqlBuilder.Parts.Inside;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class CustomizeColumnOnly : ISqlTextCustomizer
    {
        public BuildingParts Custom(BuildingParts src)
        {
            var col = src as DbColumnText;
            if (col == null) return src;
            return col.ToColumnOnly();
        }
    }
}
