namespace LambdicSql.BuilderServices.Code.Inside
{
    class RecursiveTargetParts : Parts
    {
        Parts _core;

        internal RecursiveTargetParts(Parts core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.Option.ExistRecursiveClause ?
                _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public override Parts ConcatAround(string front, string back) => new RecursiveTargetParts(_core.ConcatAround(front, back));

        public override Parts ConcatToFront(string front) => new RecursiveTargetParts(_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new RecursiveTargetParts(_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => new RecursiveTargetParts(_core.Customize(customizer));
    }
}
