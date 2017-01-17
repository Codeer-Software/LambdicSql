using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System.Linq;

namespace LambdicSql.Inside.CodeParts
{
    class StringFormatCode : ICode
    {
        string _formatText;
        ICode[] _args;
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringFormatCode(string formatText, ICode[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        StringFormatCode(string formatText, ICode[] args, string front, string back)
        {
            _formatText = formatText;
            _args = args;
            _front = front;
            _back = back;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context)
        {
            var next = context.ResetState();
            return PartsUtils.GetIndent(context.Indent) +
                _front +
                 string.Format(_formatText, _args.Select(e => e.ToString(next)).ToArray()) +
                _back;
        }

        public ICode Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
