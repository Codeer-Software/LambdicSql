using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdicSql.SqlBase.TextParts
{
    class StringFormatText : SqlText
    {
        string _formatText;
        SqlText[] _args;
        string _front = string.Empty;
        string _back = string.Empty;


        internal StringFormatText(string formatText, SqlText[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatText(string formatText, SqlText[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override SqlText ConcatAround(string front, string back)
            => new StringFormatText(_formatText, _args, front + _front, _back + back);

        public override SqlText ConcatToFront(string front)
            => new StringFormatText(_formatText, _args, front + _front, _back);

        public override SqlText ConcatToBack(string back)
            => new StringFormatText(_formatText, _args, _front, _back + back);

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
