using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class StringAddOperatorCode : Code
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorCode() { }

        StringAddOperatorCode(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + context.Option.StringAddOperator + _back;

        public override Code ConcatAround(string front, string back) => new StringAddOperatorCode(front + _front, _back + back);

        public override Code ConcatToFront(string front) => new StringAddOperatorCode(front + _front, _back);

        public override Code ConcatToBack(string back) => new StringAddOperatorCode(_front, _back + back);

        public override Code Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
