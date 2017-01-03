using System.Linq;

namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    class StringFormatText : Sentence
    {
        string _formatText;
        Sentence[] _args;
        string _front = string.Empty;
        string _back = string.Empty;


        internal StringFormatText(string formatText, Sentence[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatText(string formatText, Sentence[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override Sentence ConcatAround(string front, string back)
            => new StringFormatText(_formatText, _args, front + _front, _back + back);

        public override Sentence ConcatToFront(string front)
            => new StringFormatText(_formatText, _args, front + _front, _back);

        public override Sentence ConcatToBack(string back)
            => new StringFormatText(_formatText, _args, _front, _back + back);

        public override Sentence Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
