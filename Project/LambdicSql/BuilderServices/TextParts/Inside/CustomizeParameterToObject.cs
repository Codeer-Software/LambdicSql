namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class CustomizeParameterToObject : ISyntaxCustomizer
    {
        public TextPartsBase Custom(TextPartsBase src)
        {
            var col = src as ParameterSyntax;
            return col == null ? src : col.ToDisplayValue();
        }
    }
}
