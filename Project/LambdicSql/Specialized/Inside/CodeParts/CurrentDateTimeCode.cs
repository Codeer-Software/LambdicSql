using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CodeParts
{
    class CurrentDateTimeCode : ICode
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _core;

        internal CurrentDateTimeCode(string core)
        {
            _core = core;
        }

        CurrentDateTimeCode(string core, string front, string back)
        {
            _core = core;
            _front = front;
            _back = back;
        }

        public bool IsSingleLine(BuildingContext context) => true;

        public bool IsEmpty => false;

        public string ToString(BuildingContext context)
            => PartsUtils.GetIndent(context.Indent) + _front + "CURRENT" + context.DialectOption.CurrentDateTimeSeparator + _core + _back;
        
        public ICode Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
