namespace LambdicSql.BuilderServices.Parts.Inside
{
    internal class QueryParts : BuildingParts
    {
        BuildingParts _core;

        internal QueryParts(BuildingParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override BuildingParts ConcatAround(string front, string back) => new QueryParts(_core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new QueryParts(_core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new QueryParts(_core.ConcatToBack(back));

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
