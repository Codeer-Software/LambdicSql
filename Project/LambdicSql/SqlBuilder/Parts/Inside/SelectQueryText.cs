namespace LambdicSql.SqlBuilder.Parts.Inside
{
    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(BuildingParts core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
        {
            if (isTopLevel) return base.ToString(false, indent, context);
            return Core.ConcatAround("(", ")").ToString(false, indent, context);
        }

        public override BuildingParts ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));

        public override BuildingParts Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
