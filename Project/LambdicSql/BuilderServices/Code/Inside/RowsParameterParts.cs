namespace LambdicSql.BuilderServices.Code.Inside
{
    class RowsParameterParts : Parts
    {
        ParameterParts _core;

        internal RowsParameterParts(ParameterParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var core = context.Option.IsRowsParameterDirectValue ? _core.ToDisplayValue(false) : _core;
            return core.ToString(isTopLevel, indent, context);
        }

        public override Parts ConcatAround(string front, string back) => new RowsParameterParts((ParameterParts)_core.ConcatAround(front, back));

        public override Parts ConcatToFront(string front) => new RowsParameterParts((ParameterParts)_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new RowsParameterParts((ParameterParts)_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => new RowsParameterParts((ParameterParts)_core.Customize(customizer));
    }
}
