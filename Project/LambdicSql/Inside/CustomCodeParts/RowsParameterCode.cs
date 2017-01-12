using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class RowsParameterCode : Code
    {
        ParameterCode _core;

        internal RowsParameterCode(ParameterCode core)
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

        public override Code ConcatAround(string front, string back) => new RowsParameterCode((ParameterCode)_core.ConcatAround(front, back));

        public override Code ConcatToFront(string front) => new RowsParameterCode((ParameterCode)_core.ConcatToFront(front));

        public override Code ConcatToBack(string back) => new RowsParameterCode((ParameterCode)_core.ConcatToBack(back));

        public override Code Customize(ICodeCustomizer customizer) => new RowsParameterCode((ParameterCode)_core.Customize(customizer));
    }
}
