using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    internal class QueryParts : CodeParts
    {
        CodeParts _core;

        internal QueryParts(CodeParts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override CodeParts ConcatAround(string front, string back) => new QueryParts(_core.ConcatAround(front, back));

        public override CodeParts ConcatToFront(string front) => new QueryParts(_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new QueryParts(_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new QueryParts(_core.Customize(customizer));
    }
}
