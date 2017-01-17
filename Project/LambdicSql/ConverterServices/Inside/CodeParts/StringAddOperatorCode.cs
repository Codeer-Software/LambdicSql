using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.ConverterServices.Inside.CodeParts
{
    class StringAddOperatorCode : ICode
    {
        internal StringAddOperatorCode() { }

        public bool IsEmpty => false;

        public bool IsSingleLine(BuildingContext context) => true;

        public string ToString(BuildingContext context) => PartsUtils.GetIndent(context.Indent) + context.DialectOption.StringAddOperator;

        public ICode Accept(ICodeCustomizer customizer) => customizer.Visit(this);
    }
}
