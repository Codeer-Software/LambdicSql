using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class SqlCode : ICode, ISqlCode
    {
        ICode _core;

        internal SqlCode(ICode core)
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
            return new SqlCode(_core.Accept(customizer));
        }

        internal static ICode Create(ICode core)
        {
            var topQuery = core as ITopQueryCode;
            return topQuery == null ? new SqlCode(core) : (ICode)new TopQuerySqlCode(topQuery);
        }
    }
}
