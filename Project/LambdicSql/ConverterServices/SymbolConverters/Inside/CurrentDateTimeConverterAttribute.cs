using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class CurrentDateTimeConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }

        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter) => new CurrentDateTimeParts(Name);
    }
}
