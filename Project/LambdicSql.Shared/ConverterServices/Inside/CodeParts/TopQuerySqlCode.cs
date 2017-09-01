using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class TopQuerySqlCode : ITopQueryCode, ISqlCode
    {
        ITopQueryCode _core;
        public ICode Core => _core.Core;

        public bool IsEmpty => Core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context) => _core.ToString(context);

        internal TopQuerySqlCode(ITopQueryCode core)
        {
            _core = core;
        }

        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new TopQuerySqlCode((ITopQueryCode)_core.Accept(customizer));
        }

        public ITopQueryCode Create(ICode core)
            => new TopQuerySqlCode(_core.Create(core));
    }
}
