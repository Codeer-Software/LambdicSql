using System.Linq;

namespace LambdicSql.SqlBuilder.Parts.Inside
{
    class StringAddOperatorText : BuildingParts
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorText() { }

        StringAddOperatorText(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + context.Option.StringAddOperator + _back;

        public override BuildingParts ConcatAround(string front, string back)
            => new StringAddOperatorText(front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front)
            => new StringAddOperatorText(front + _front, _back);

        public override BuildingParts ConcatToBack(string back)
            => new StringAddOperatorText(_front, _back + back);

        public override BuildingParts Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
