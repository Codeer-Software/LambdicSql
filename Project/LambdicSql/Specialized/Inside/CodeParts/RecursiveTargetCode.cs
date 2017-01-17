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

        public string ToString(BuildingContext context)
            => context.DialectOption.ExistRecursiveClause ?
                new AroundCode(_core, "RECURSIVE ", string.Empty).ToString(context):
                _core.ToString(context);

        public ICode Accept(ICodeCustomizer customizer) => new RecursiveTargetCode(_core.Accept(customizer));
    }
}
