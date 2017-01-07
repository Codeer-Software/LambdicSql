namespace LambdicSql.BuilderServices.Code.Inside
{ 
    class DisableBracketsParts : Parts
    {
        Parts _core;

        internal DisableBracketsParts(Parts core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Parts ConcatAround(string front, string back) => this;

        public override Parts ConcatToFront(string front) => new DisableBracketsParts(_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new DisableBracketsParts(_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
