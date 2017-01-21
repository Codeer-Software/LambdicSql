using System.Linq;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class StringAddOperatorSyntax : TextPartsBase
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorSyntax() { }

        StringAddOperatorSyntax(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => SyntaxUtils.GetIndent(indent) + _front + context.Option.StringAddOperator + _back;

        public override TextPartsBase ConcatAround(string front, string back) => new StringAddOperatorSyntax(front + _front, _back + back);

        public override TextPartsBase ConcatToFront(string front) => new StringAddOperatorSyntax(front + _front, _back);

        public override TextPartsBase ConcatToBack(string back) => new StringAddOperatorSyntax(_front, _back + back);

        public override TextPartsBase Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}
