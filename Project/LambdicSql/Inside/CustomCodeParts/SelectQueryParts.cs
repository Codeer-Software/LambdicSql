using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    internal class SelectQueryParts : Parts
    {
        Parts _core;

        internal SelectQueryParts(Parts core)
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

        public override Parts ConcatAround(string front, string back) => new SelectQueryParts(_core.ConcatAround(front, back));

        public override Parts ConcatToFront(string front) => new SelectQueryParts(_core.ConcatToFront(front));

        public override Parts ConcatToBack(string back) => new SelectQueryParts(_core.ConcatToBack(back));

        public override Parts Customize(IPartsCustomizer customizer) => new SelectQueryParts(_core.Customize(customizer));
    }
}
