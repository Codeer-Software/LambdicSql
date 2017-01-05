namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class CustomizeColumnOnly : ISyntaxCustomizer
    {
        public Syntax Custom(Syntax src)
        {
            var col = src as DbColumnSyntax;
            return col == null ? src : col.ToColumnOnly();
        }
    }
}
