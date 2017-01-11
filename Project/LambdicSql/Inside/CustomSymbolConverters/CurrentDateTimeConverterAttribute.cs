using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class CurrentDateTimeConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }

        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter) => new CurrentDateTimeParts(Name);
    }
}
