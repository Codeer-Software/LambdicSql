using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using LambdicSql.BuilderServices.Parts.Inside;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxSelectAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = expression.SkipMethodChain(0); i < expression.Arguments.Count - 1; i++)
            {
                modify.Add(expression.Arguments[i]);
            }

            var select = LineSpace(new BuildingParts[] { "SELECT" }.Concat(modify.Select(e => converter.Convert(e))).ToArray());

            //select elemnts.
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];
            BuildingParts selectTargetText = null;
            ObjectCreateInfo createInfo = null;

            //*
            if (typeof(IAsterisk).IsAssignableFrom(selectTargets.Type))
            {
                var asteriskType = selectTargets.Type.IsGenericType ? selectTargets.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(IAsterisk<>)) createInfo = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                select.Add("*");
            }
            //new { item = db.tbl.column }
            else
            {
                createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
                selectTargetText =
                    new VParts(createInfo.Members.Select(e => ToStringSelectedElement(converter, e)).ToArray()) { Indent = 1, Separator = "," };
            }

            return new SelectClauseParts(createInfo, selectTargetText == null ? (BuildingParts)select : new VParts(select, selectTargetText));
        }

        static BuildingParts ToStringSelectedElement(ExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.Convert(element.Expression);

            //normal select.
            return LineSpace(converter.Convert(element.Expression), "AS", element.Name);
        }

    }
}
