using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class StringAddOperatorCode : ICode
    {
        string _front = string.Empty;
        string _back = string.Empty;

        internal StringAddOperatorCode() { }

        StringAddOperatorCode(string front, string back)
        {
            _front = front;
            _back = back;
        }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + context.Option.StringAddOperator + _back;

        public ICode Customize(ICodeCustomizer customizer) => customizer.Custom(this);
    }
}
