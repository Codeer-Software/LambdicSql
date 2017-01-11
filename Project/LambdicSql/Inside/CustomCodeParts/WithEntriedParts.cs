using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class WithEntriedParts : Parts
    {
        Parts _core;
        string[] _names;

        internal WithEntriedParts(Parts core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            foreach (var e in _names) context.WithEntied[e] = true;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override Parts ConcatAround(string front, string back) => new WithEntriedParts(_core.ConcatAround(front, back), _names);

        public override Parts ConcatToFront(string front) => new WithEntriedParts(_core.ConcatToFront(front), _names);

        public override Parts ConcatToBack(string back) => new WithEntriedParts(_core.ConcatToBack(back), _names);

        public override Parts Customize(IPartsCustomizer customizer) => new WithEntriedParts(_core.Customize(customizer), _names);
    }
}
