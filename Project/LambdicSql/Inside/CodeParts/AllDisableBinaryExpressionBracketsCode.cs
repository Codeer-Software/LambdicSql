using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;

namespace LambdicSql.Inside.CodeParts
{
    class AllDisableBinaryExpressionBracketsCode : Code, IDisableBinaryExpressionBrackets
    {
        Code _core;

        internal AllDisableBinaryExpressionBracketsCode(Code core)
        {
            _core = core;
        }

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public override Code Customize(ICodeCustomizer customizer) => new AllDisableBinaryExpressionBracketsCode(_core.Customize(customizer));
    }
}
