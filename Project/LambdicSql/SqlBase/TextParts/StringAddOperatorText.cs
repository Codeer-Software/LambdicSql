using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    class StringAddOperatorText : SqlText
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorText() { }

        StringAddOperatorText(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + context.Option.StringAddOperator + _back;

        public override SqlText ConcatAround(string front, string back)
            => new StringAddOperatorText(front + _front, _back + back);

        public override SqlText ConcatToFront(string front)
            => new StringAddOperatorText(front + _front, _back);

        public override SqlText ConcatToBack(string back)
            => new StringAddOperatorText(_front, _back + back);

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
