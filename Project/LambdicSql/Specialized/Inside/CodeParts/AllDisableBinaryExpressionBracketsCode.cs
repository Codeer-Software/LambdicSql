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

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public bool IsEmpty => _core.IsEmpty;

        public string ToString(BuildingContext context) => _core.ToString(context);

        public ICode Customize(ICodeCustomizer customizer) => new AllDisableBinaryExpressionBracketsCode(_core.Customize(customizer));
    }
}
