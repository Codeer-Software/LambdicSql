namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class CustomizeParameterToObject : ISyntaxCustomizer
    {
        public Syntax Custom(Syntax src)
        {
            var col = src as ParameterSyntax;
            return col == null ? src : col.ToDisplayValue();
        }
    }
}
