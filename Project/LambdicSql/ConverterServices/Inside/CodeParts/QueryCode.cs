using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    internal class QueryCode : ICode
    {
        ICode _core;

        internal QueryCode(ICode core)
        {
            _core = core;
        }

        public bool IsEmpty => _core.IsEmpty;

        public bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        public string ToString(BuildingContext context) => _core.ToString(context.ChangeTopLevelQuery(false));

        public ICode Accept(ICodeCustomizer customizer) => new QueryCode(_core.Accept(customizer));
    }
}
