using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class CurrentDateTimeCode : ICode
    {
        string _core;

        internal CurrentDateTimeCode(string core)
        {
            _core = core;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context)
            => PartsUtils.GetIndent(context.Indent) + "CURRENT" + context.DialectOption.CurrentDateTimeSeparator + _core;
        
        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}
