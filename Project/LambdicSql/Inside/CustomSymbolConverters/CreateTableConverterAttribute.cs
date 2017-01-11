﻿using LambdicSql.BuilderServices.Parts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CustomCodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    //TODO CustomizeParameterToObjectの特殊版をつくるかな・・・　、　そんでパラメータでもOKなDBが一個あったよね　そんでCREATE DBとかの微妙仕様はやめる
    class CreateTableConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
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