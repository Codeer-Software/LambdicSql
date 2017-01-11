using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;
using System.Linq;

namespace LambdicSql.Inside.CustomCodeParts
{
    class StringFormatParts : CodeParts
    {
        string _formatText;
        CodeParts[] _args;
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringFormatParts(string formatText, CodeParts[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatParts(string formatText, CodeParts[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => PartsUtils.GetIndent(indent) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override CodeParts ConcatAround(string front, string back) => new StringFormatParts(_formatText, _args, front + _front, _back + back);

        public override CodeParts ConcatToFront(string front) => new StringFormatParts(_formatText, _args, front + _front, _back);

        public override CodeParts ConcatToBack(string back) => new StringFormatParts(_formatText, _args, _front, _back + back);

        public override CodeParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
