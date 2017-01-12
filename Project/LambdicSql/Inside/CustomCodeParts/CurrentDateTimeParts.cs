using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class CurrentDateTimeParts : Parts
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _core;

        internal CurrentDateTimeParts(string core)
        {
            _core = core;
        }

        CurrentDateTimeParts(string core, string front, string back)
        {
            _core = core;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(BuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => PartsUtils.GetIndent(indent) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

        public override Parts ConcatAround(string front, string back) => new CurrentDateTimeParts(_core, front + _front, _back + back);

        public override Parts ConcatToFront(string front) => new CurrentDateTimeParts(_core, front + _front, _back);

        public override Parts ConcatToBack(string back) => new CurrentDateTimeParts(_core, _front, _back + back);

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
