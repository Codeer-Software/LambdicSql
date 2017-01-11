using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.Inside.CustomCodeParts.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class ReferencesConverter : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var create = LineSpace("REFERENCES", converter.Convert(expression.Arguments[startIndex]));
            var args = expression.Arguments.Skip(startIndex + 1).Select(e => converter.Convert(e).Customize(new CustomizeColumnOnly())).ToArray();
            var func = Func(create, args);
            func.Indent = 1;
            return func;
        }
    }
}
