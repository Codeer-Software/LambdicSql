using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Inside.SymbolConverters
{
    class CurrentDateTimeConverterAttribute : MethodConverterAttribute
    {
        public string Name { get; set; }

        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter) => new CurrentDateTimeCode(Name);
    }
}
