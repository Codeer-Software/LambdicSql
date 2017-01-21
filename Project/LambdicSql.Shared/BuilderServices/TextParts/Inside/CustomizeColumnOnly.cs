namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class CustomizeColumnOnly : ISyntaxCustomizer
    {
        public TextPartsBase Custom(TextPartsBase src)
        {
            var col = src as DbColumnSyntax;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}
