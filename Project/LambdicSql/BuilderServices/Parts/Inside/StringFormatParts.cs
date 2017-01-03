using System.Linq;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    class StringFormatParts : BuildingParts
    {
        string _formatText;
        BuildingParts[] _args;
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringFormatParts(string formatText, BuildingParts[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatParts(string formatText, BuildingParts[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            => BuildingPartsUtils.GetIndent(indent) + 
            _front +
             string.Format(_formatText, _args.Select(e => e.ToString(true, 0, context)).ToArray()) +
            _back;

        public override BuildingParts ConcatAround(string front, string back) => new StringFormatParts(_formatText, _args, front + _front, _back + back);

        public override BuildingParts ConcatToFront(string front) => new StringFormatParts(_formatText, _args, front + _front, _back);

        public override BuildingParts ConcatToBack(string back) => new StringFormatParts(_formatText, _args, _front, _back + back);

        public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
    }
}
