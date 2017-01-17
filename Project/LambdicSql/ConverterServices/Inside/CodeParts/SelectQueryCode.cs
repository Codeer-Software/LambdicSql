using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    internal class SelectQueryCode : ICode
    {
        ICode _core;

        internal SelectQueryCode(ICode core)
        {
            _core = core;
        }

        public bool IsEmpty => _core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var target = isTopLevel ? _core : new AroundCode(_core, "(", ")");
            return target.ToString(false, indent, context);
        }

        public ICode Customize(ICodeCustomizer customizer) => new SelectQueryCode(_core.Customize(customizer));
    }
}
