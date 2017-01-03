namespace LambdicSql.BuilderServices.Parts.Inside
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(BuildingParts core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) 
            => base.ToString(false, indent, context);

        public override BuildingParts ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        public override BuildingParts ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        public override BuildingParts ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));

        public override BuildingParts Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
