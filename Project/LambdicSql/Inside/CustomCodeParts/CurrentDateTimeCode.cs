using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CurrentDateTimeCode : Code
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

        public override bool IsSingleLine(BuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => PartsUtils.GetIndent(indent) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

        public override Code ConcatAround(string front, string back) => new CurrentDateTimeCode(_core, front + _front, _back + back);

        public override Code ConcatToFront(string front) => new CurrentDateTimeCode(_core, front + _front, _back);

        public override Code ConcatToBack(string back) => new CurrentDateTimeCode(_core, _front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
