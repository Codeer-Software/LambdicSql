using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class RowsParameterParts : CodeParts
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

        public override CodeParts ConcatAround(string front, string back) => new RowsParameterParts((ParameterParts)_core.ConcatAround(front, back));

        public override CodeParts ConcatToFront(string front) => new RowsParameterParts((ParameterParts)_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new RowsParameterParts((ParameterParts)_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new RowsParameterParts((ParameterParts)_core.Customize(customizer));
    }
}
