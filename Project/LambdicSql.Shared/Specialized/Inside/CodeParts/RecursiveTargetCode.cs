using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class RecursiveTargetCode : ICode
    {
        ICode _core;
        bool _existRecursiveClause;

        internal RecursiveTargetCode(ICode core, bool existRecursiveClause)
        {
            _core = core;
            _existRecursiveClause = existRecursiveClause;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context)
            => _existRecursiveClause ?
                new AroundCode(_core, "RECURSIVE ", string.Empty).ToString(context) :
                _core.ToString(context);

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new RecursiveTargetCode(_core.Accept(customizer), _existRecursiveClause);
        }
    }
}
