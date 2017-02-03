using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    internal class SelectQueryCode : ICode
    {
        internal ICode Core { get; private set; }

        internal SelectQueryCode(ICode core)
        {
            Core = core;
        }

        public bool IsEmpty => Core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => Core.IsSingleLine(context);

        public string ToString(BuildingContext context)
        {
            var target = context.IsTopLevelQuery ? Core : new AroundCode(Core, "(", ")");
            return target.ToString(context.ChangeTopLevelQuery(false));
        }
        
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new SelectQueryCode(Core.Accept(customizer));
        }
    }
}
