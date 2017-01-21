using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdicSql.SqlBase.TextParts
{
    class StringFormatText : ExpressionElement
    {
        string _formatText;
        ExpressionElement[] _args;
        string _front = string.Empty;
        string _back = string.Empty;


        internal StringFormatText(string formatText, ExpressionElement[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatText(string formatText, ExpressionElement[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine(ExpressionConvertingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override ExpressionElement ConcatAround(string front, string back)
            => new StringFormatText(_formatText, _args, front + _front, _back + back);

        public override ExpressionElement ConcatToFront(string front)
            => new StringFormatText(_formatText, _args, front + _front, _back);

        public override ExpressionElement ConcatToBack(string back)
            => new StringFormatText(_formatText, _args, _front, _back + back);

        public override ExpressionElement Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
