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

        public string ToString(BuildingContext context)
        {
            var target = context.IsTopLevelQuery ? _core : new AroundCode(_core, "(", ")");
            return target.ToString(context.ChangeTopLevelQuery(false));
        }

        public ICode Customize(ICodeCustomizer customizer) => new SelectQueryCode(_core.Customize(customizer));
    }
}
