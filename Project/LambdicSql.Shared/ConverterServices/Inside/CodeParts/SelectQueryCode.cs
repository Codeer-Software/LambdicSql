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
        
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new SelectQueryCode(_core.Accept(customizer));
        }
    }
}
