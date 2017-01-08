using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    //TODO CustomizeParameterToObjectの特殊版をつくるかな・・・　、　そんでパラメータでもOKなDBが一個あったよね　そんでCREATE DBとかの微妙仕様はやめる
    class CreateTableConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var create = LineSpace("CREATE TABLE", converter.Convert(expression.Arguments[0]));
            var args = expression.Arguments.Skip(1).Select(e => converter.Convert(e).Customize(new CustomizeColumnOnly()).Customize(new CustomizeParameterToObject())).ToArray();
            return Func(create, args);
        }
    }
}
