using System.Linq;

namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    class StringAddOperatorText : Sentence
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

        public override Sentence ConcatAround(string front, string back)
            => new StringAddOperatorText(front + _front, _back + back);

        public override Sentence ConcatToFront(string front)
            => new StringAddOperatorText(front + _front, _back);

        public override Sentence ConcatToBack(string back)
            => new StringAddOperatorText(_front, _back + back);

        public override Sentence Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
