namespace LambdicSql.BuilderServices.Code.Inside
{
    class CustomizeParameterToObject : IPartsCustomizer
    {
        public Parts Custom(Parts src)
        {
            var col = src as ParameterParts;
            return col == null ? src : col.ToDisplayValue();
        }
    }
}
