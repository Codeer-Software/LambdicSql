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
        public string Name { get; set; }
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var create = LineSpace(Name, converter.Convert(expression.Arguments[0]));
            var args = ((NewArrayExpression)expression.Arguments[1]).Expressions.
                Select(e => converter.Convert(e).Customize(new CustomizeColumnOnly()).Customize(new CustomizeParameterToObject(true))).ToArray();

            var clause = new VParts(create.ConcatToBack("("));
            var argsParts = new VParts() { Separator = ","};
            argsParts.AddRange(1, args);
            clause.Add(argsParts.ConcatToBack(")"));

            return clause;
        }
    }
}
