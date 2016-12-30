using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    class StringAddOperatorText : ExpressionElement
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorText() { }

        StringAddOperatorText(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(ExpressionConvertingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + context.Option.StringAddOperator + _back;

        public override ExpressionElement ConcatAround(string front, string back)
            => new StringAddOperatorText(front + _front, _back + back);

        public override ExpressionElement ConcatToFront(string front)
            => new StringAddOperatorText(front + _front, _back);

        public override ExpressionElement ConcatToBack(string back)
            => new StringAddOperatorText(_front, _back + back);

        public override ExpressionElement Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
