using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    internal class SelectQueryParts : CodeParts
    {
        CodeParts _core;

        internal SelectQueryParts(CodeParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var target = isTopLevel ? _core : _core.ConcatAround("(", ")");
            return target.ToString(false, indent, context);
        }

        public override CodeParts ConcatAround(string front, string back) => new SelectQueryParts(_core.ConcatAround(front, back));

        public override CodeParts ConcatToFront(string front) => new SelectQueryParts(_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new SelectQueryParts(_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new SelectQueryParts(_core.Customize(customizer));
    }
}
