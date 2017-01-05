namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    internal class QuerySyntax : TextPartsBase
    {
        TextPartsBase _core;

        internal QuerySyntax(TextPartsBase core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override TextPartsBase ConcatAround(string front, string back) => new QuerySyntax(_core.ConcatAround(front, back));

        public override TextPartsBase ConcatToFront(string front) => new QuerySyntax(_core.ConcatToFront(front));

        public override TextPartsBase ConcatToBack(string back) => new QuerySyntax(_core.ConcatToBack(back));

        public override TextPartsBase Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}
