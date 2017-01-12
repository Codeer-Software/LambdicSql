using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class StringAddOperatorParts : Parts
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorParts() { }

        StringAddOperatorParts(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + context.Option.StringAddOperator + _back;

        public override Parts ConcatAround(string front, string back) => new StringAddOperatorParts(front + _front, _back + back);

        public override Parts ConcatToFront(string front) => new StringAddOperatorParts(front + _front, _back);

        public override Parts ConcatToBack(string back) => new StringAddOperatorParts(_front, _back + back);

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
