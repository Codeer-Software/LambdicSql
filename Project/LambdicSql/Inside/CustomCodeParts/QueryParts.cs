using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    internal class QueryParts : Parts
    {
        Parts _core;

        internal QueryParts(Parts core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Parts ConcatAround(string front, string back) => new QueryParts(_core.ConcatAround(front, back));

        public override Parts ConcatToFront(string front) => new QueryParts(_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new QueryParts(_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => new QueryParts(_core.Customize(customizer));
    }
}
