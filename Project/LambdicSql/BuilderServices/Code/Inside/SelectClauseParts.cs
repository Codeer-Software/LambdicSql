namespace LambdicSql.BuilderServices.Code.Inside
{
    class SelectClauseParts : Parts
    {
        Parts _core;

        internal SelectClauseParts(Parts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(isTopLevel, indent, context);

        public override Parts ConcatAround(string front, string back) => new SelectClauseParts(_core.ConcatAround(front, back));

        public override Parts ConcatToFront(string front) => new SelectClauseParts(_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new SelectClauseParts(_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
