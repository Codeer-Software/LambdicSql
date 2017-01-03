namespace LambdicSql.BuilderServices.Parts.Inside
{
    class CustomizeColumnOnly : IPartsCustomizer
    {
        public BuildingParts Custom(BuildingParts src)
        {
            var col = src as DbColumnParts;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}
