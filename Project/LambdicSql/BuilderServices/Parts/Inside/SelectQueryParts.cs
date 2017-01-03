namespace LambdicSql.BuilderServices.Parts.Inside
{
    internal class SelectQueryParts : BuildingParts
    {
        BuildingParts _core;

        internal SelectQueryParts(BuildingParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var target = isTopLevel ? _core : _core.ConcatAround("(", ")");
            return target.ToString(false, indent, context);
        }

        public override BuildingParts ConcatAround(string front, string back) => new SelectQueryParts(_core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new SelectQueryParts(_core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new SelectQueryParts(_core.ConcatToBack(back));

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
