using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.Inside.CustomCodeParts
{
    class StringAddOperatorParts : CodeParts
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

        public override CodeParts ConcatAround(string front, string back) => new StringAddOperatorParts(front + _front, _back + back);

        public override CodeParts ConcatToFront(string front) => new StringAddOperatorParts(front + _front, _back);

        public override CodeParts ConcatToBack(string back) => new StringAddOperatorParts(_front, _back + back);

        public override CodeParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
