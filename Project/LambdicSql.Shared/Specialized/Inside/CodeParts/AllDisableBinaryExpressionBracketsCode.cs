using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;

namespace LambdicSql.Inside.CodeParts
{
    class AllDisableBinaryExpressionBracketsCode : ICode, IDisableBinaryExpressionBrackets
    {
        ICode _core;

        internal AllDisableBinaryExpressionBracketsCode(ICode core)
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
            return new AllDisableBinaryExpressionBracketsCode(_core.Accept(customizer));
        }
    }
}
