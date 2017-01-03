namespace LambdicSql.BuilderServices.Parts.Inside
{
    class CustomizeParameterToObject : IPartsCustomizer
    {
        public BuildingParts Custom(BuildingParts src)
        {
            var col = src as ParameterParts;
            return col == null ? src : col.ToDisplayValue();
        }
    }
}
