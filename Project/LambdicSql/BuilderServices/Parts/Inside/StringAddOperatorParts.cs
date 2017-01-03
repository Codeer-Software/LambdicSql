using System.Linq;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    class StringAddOperatorParts : BuildingParts
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

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => BuildingPartsUtils.GetIndent(indent) + _front + context.Option.StringAddOperator + _back;

        public override BuildingParts ConcatAround(string front, string back) => new StringAddOperatorParts(front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front) => new StringAddOperatorParts(front + _front, _back);

        public override BuildingParts ConcatToBack(string back) => new StringAddOperatorParts(_front, _back + back);

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
