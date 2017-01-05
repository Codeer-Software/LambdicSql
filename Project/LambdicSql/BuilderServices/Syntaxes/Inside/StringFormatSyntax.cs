using System.Linq;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class StringFormatSyntax : Syntax
    {
        string _formatText;
        Syntax[] _args;
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringFormatSyntax(string formatText, Syntax[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatSyntax(string formatText, Syntax[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => SyntaxUtils.GetIndent(indent) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override Syntax ConcatAround(string front, string back) => new StringFormatSyntax(_formatText, _args, front + _front, _back + back);

        public override Syntax ConcatToFront(string front) => new StringFormatSyntax(_formatText, _args, front + _front, _back);

        public override Syntax ConcatToBack(string back) => new StringFormatSyntax(_formatText, _args, _front, _back + back);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}
