using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax.Inside
{
    class SqlSyntaxSelectAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = method.SkipMethodChain(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }

            var select = LineSpace(new ExpressionElement[] { "SELECT" }.Concat(modify.Select(e => converter.Convert(e))).ToArray());

            //select elemnts.
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            ExpressionElement selectTargetText = null;
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
                    new VText(createInfo.Members.Select(e => ToStringSelectedElement(converter, e)).ToArray()) { Indent = 1, Separator = "," };
            }

            return new SelectClauseText(createInfo, selectTargetText == null ? (ExpressionElement)select : new VText(select, selectTargetText));
        }

        static ExpressionElement ToStringSelectedElement(IExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.Convert(element.Expression);

            //normal select.
            return LineSpace(converter.Convert(element.Expression), "AS", element.Name);
        }

    }
}
