using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class RecursiveTargetParts : CodeParts
    {
        CodeParts _core;

        internal RecursiveTargetParts(CodeParts core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.Option.ExistRecursiveClause ?
                _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public override CodeParts ConcatAround(string front, string back) => new RecursiveTargetParts(_core.ConcatAround(front, back));

        public override CodeParts ConcatToFront(string front) => new RecursiveTargetParts(_core.ConcatToFront(front));

        public override CodeParts ConcatToBack(string back) => new RecursiveTargetParts(_core.ConcatToBack(back));

        public override CodeParts Customize(IPartsCustomizer customizer) => new RecursiveTargetParts(_core.Customize(customizer));
    }
}
