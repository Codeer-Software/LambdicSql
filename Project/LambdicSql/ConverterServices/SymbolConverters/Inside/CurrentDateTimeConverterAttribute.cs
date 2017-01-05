using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class CurrentDateTimeConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }

        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter) => new CurrentDateTimeSyntax(Name);
    }
}
