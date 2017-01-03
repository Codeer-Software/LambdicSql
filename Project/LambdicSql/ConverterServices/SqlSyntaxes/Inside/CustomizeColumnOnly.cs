using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
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
