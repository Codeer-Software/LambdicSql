using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class RecursiveTargetCode : ICode
    {
        ICode _core;

        internal RecursiveTargetCode(ICode core)
        {
            _core = core;
        }

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public bool IsEmpty => false;

        public string ToString(bool isTopLevel, int indent, BuildingContext context)
            => context.Option.ExistRecursiveClause ?
                new AroundCode(_core, "RECURSIVE ", string.Empty).ToString(isTopLevel, indent, context):
                _core.ToString(isTopLevel, indent, context);

        public ICode Customize(ICodeCustomizer customizer) => new RecursiveTargetCode(_core.Customize(customizer));
    }
}
