using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
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
                new AroundCode(_core, "RECURSIVE ", string.Empty).ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public override Code Customize(ICodeCustomizer customizer) => new RecursiveTargetCode(_core.Customize(customizer));
    }
}
