using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    internal class SelectQueryCode : Code
    {
        Code _core;

        internal SelectQueryCode(Code core)
        {
            _core = core;
        }

        public override bool IsEmpty => _core.IsEmpty;

        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var target = isTopLevel ? _core : new AroundCode(_core, "(", ")");
            return target.ToString(false, indent, context);
        }

        public override Code Customize(ICodeCustomizer customizer) => new SelectQueryCode(_core.Customize(customizer));
    }
}
