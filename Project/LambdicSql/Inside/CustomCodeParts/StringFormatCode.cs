using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System.Linq;

namespace LambdicSql.Inside.CustomCodeParts
{
    class StringFormatCode : Code
    {
        string _formatText;
        Code[] _args;
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringFormatCode(string formatText, Code[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatCode(string formatText, Code[] args, string front, string back)
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

        public override Code ConcatAround(string front, string back) => new StringFormatCode(_formatText, _args, front + _front, _back + back);

        public override Code ConcatToFront(string front) => new StringFormatCode(_formatText, _args, front + _front, _back);

        public override Code ConcatToBack(string back) => new StringFormatCode(_formatText, _args, _front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
