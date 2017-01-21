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

        internal StringFormatCode(string formatText, ICode[] args)
        {
            _formatText = formatText;
            _args = args;
        }

        public bool IsSingleLine(BuildingContext context) => true;

        public bool IsEmpty => false;

        public string ToString(BuildingContext context)
        {
            var next = context.ChangeTopLevelQuery(true).ChangeIndent(0);
            return PartsUtils.GetIndent(context.Indent) +
                 string.Format(_formatText, _args.Select(e => e.ToString(next)).ToArray());
        }

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}
