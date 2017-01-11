using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{ 
    class DisableBracketsParts : CodeParts
    {
        CodeParts _core;

        internal DisableBracketsParts(CodeParts core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override CodeParts ConcatAround(string front, string back) => this;

        public override CodeParts ConcatToFront(string front) => new DisableBracketsParts(_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new DisableBracketsParts(_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new DisableBracketsParts(_core.Customize(customizer));
    }
}
