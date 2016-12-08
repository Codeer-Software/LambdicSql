using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside.Keywords
{
    static class SelectClause
    {
        internal static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];

            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = method.AdjustSqlSyntaxMethodArgumentIndex(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }

            //select core.
            var selectTarget = method.Arguments[method.Arguments.Count - 1];
            var selectTargetText = string.Empty;
            ObjectCreateInfo createInfo = null;

            //*
            if (typeof(Asterisk).IsAssignableFrom(selectTarget.Type))
            {
                var asteriskType = selectTarget.Type.IsGenericType ? selectTarget.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(Asterisk<>)) createInfo = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                selectTargetText = " *";
            }
            //new { item = db.tbl.column }
            else
            {
                createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTarget);
                selectTargetText = Environment.NewLine + "\t" +
                    string.Join("," + Environment.NewLine + "\t", createInfo.Members.Select(e => ToStringSelectedElement(converter, e)).ToArray());
            }

            //remember creat info.
            if (converter.Context.ObjectCreateInfo == null) converter.Context.ObjectCreateInfo = createInfo;

            //return select clause text.
            return Environment.NewLine + string.Join(" ", new[] { "SELECT" }.Concat(modify.Select(e => converter.ToString(e))).ToArray()) + selectTargetText;
        }

        static string ToStringSelectedElement(ISqlStringConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.ToString(element.Expression);

            //normal select.
            return converter.ToString(element.Expression) + " AS " + element.Name;
        }
    }
}
