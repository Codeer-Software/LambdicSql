using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class RecursiveTargetCode : Code
    {
        Code _core;

        internal RecursiveTargetCode(Code core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.Option.ExistRecursiveClause ?
                _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public override Code ConcatAround(string front, string back) => new RecursiveTargetCode(_core.ConcatAround(front, back));

        public override Code ConcatToFront(string front) => new RecursiveTargetCode(_core.ConcatToFront(front));

        public override Code ConcatToBack(string back) => new RecursiveTargetCode(_core.ConcatToBack(back));

        public override Code Customize(ICodeCustomizer customizer) => new RecursiveTargetCode(_core.Customize(customizer));
    }
}
