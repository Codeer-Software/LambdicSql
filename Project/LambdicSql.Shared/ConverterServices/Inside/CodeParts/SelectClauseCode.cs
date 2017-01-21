using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class SelectClauseCode : ICode
    {
        ICode _core;

        internal SelectClauseCode(ICode core)
        {
            _core = core;
        }

        public bool IsEmpty => _core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context) => _core.ToString(context);

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new SelectClauseCode(_core.Accept(customizer));
        }
    }
}
