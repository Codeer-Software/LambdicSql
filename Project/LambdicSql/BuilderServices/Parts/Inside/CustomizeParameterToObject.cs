namespace LambdicSql.BuilderServices.Parts.Inside
{
    //TODO 厳密に考えると、組み合わせ前にやっちゃったらばグル
    class CustomizeParameterToObject : ISqlTextCustomizer
    {
        public BuildingParts Custom(BuildingParts src)
        {
            var col = src as ParameterText;
            if (col == null) return src;
            return col.ToDisplayValue();
        }
    }
}
