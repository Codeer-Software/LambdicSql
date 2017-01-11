using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class SelectClauseParts : CodeParts
    {
        CodeParts _core;

        internal SelectClauseParts(CodeParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(isTopLevel, indent, context);

        public override CodeParts ConcatAround(string front, string back) => new SelectClauseParts(_core.ConcatAround(front, back));

        public override CodeParts ConcatToFront(string front) => new SelectClauseParts(_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new SelectClauseParts(_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new SelectClauseParts(_core.Customize(customizer));
    }
}
