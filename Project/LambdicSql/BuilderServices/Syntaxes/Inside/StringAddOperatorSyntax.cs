using System.Linq;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class StringAddOperatorSyntax : Syntax
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

        public override Syntax ConcatAround(string front, string back) => new StringAddOperatorSyntax(front + _front, _back + back);

        public override Syntax ConcatToFront(string front) => new StringAddOperatorSyntax(front + _front, _back);

        public override Syntax ConcatToBack(string back) => new StringAddOperatorSyntax(_front, _back + back);

        public override Syntax Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);
    }
}
