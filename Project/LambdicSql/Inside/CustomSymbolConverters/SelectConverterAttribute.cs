using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CustomCodeParts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class SelectConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            //ALL, DISTINCT, TOP
            var modify = new List<Expression>();
            for (int i = expression.SkipMethodChain(0); i < expression.Arguments.Count - 1; i++)
            {
                modify.Add(expression.Arguments[i]);
            }

            var select = LineSpace(new Parts[] { "SELECT" }.Concat(modify.Select(e => converter.Convert(e))).ToArray());

            //select elemnts.
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];

            //*
            if (typeof(IAsterisk).IsAssignableFrom(selectTargets.Type))
            {
                select.Add("*");
                return new SelectClauseParts(select);
            }

            //new { item = db.tbl.column }
            else
            {
                var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
                var elements = new VParts(createInfo.Members.Select(e => ConvertSelectedElement(converter, e))) { Indent = 1, Separator = "," };
                return new SelectClauseParts(new VParts(select, elements));
            }
        }

        static Parts ConvertSelectedElement(ExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.Convert(element.Expression);

            //normal select.
            return LineSpace(converter.Convert(element.Expression), "AS", element.Name);
        }
    }
}
