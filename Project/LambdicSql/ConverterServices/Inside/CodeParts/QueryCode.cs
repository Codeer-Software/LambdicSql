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

        public string ToString(bool isTopLevel, int indent, BuildingContext context) => _core.ToString(false, indent, context);

        public ICode Customize(ICodeCustomizer customizer) => new QueryCode(_core.Customize(customizer));
    }
}
