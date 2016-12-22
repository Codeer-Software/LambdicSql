using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;
using System;
using LambdicSql;

namespace LambdicSql.Inside.Keywords
{
    static class SelectClause
    {
        internal static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];

            /*
            if (method.Arguments.Count == 2 && 
                method.Method.GetParameters()[1].ParameterType == typeof(object[]))
            {
                return LineSpace("SELECT", converter.Convert(method.Arguments[1]));
            }
            */

            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = method.SkipMethodChain(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }

            var select = LineSpace(new SqlText[] { "SELECT" }.Concat(modify.Select(e => converter.Convert(e))).ToArray());

            //select elemnts.
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            SqlText selectTargetText = null;
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

            return new SelectClauseText(createInfo, selectTargetText == null ? (SqlText)select : new VText(select, selectTargetText));
        }

        static SqlText ToStringSelectedElement(ISqlStringConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.Convert(element.Expression);
            
            //normal select.
            return LineSpace(converter.Convert(element.Expression), "AS", element.Name);
        }

        internal static SqlText ConvertRecursive(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (SqlText)e.Name).ToArray()));
        }
    }

    class RecursiveClauseText : SqlText
    {
        SqlText _core;
        ObjectCreateInfo _createInfo;

        internal RecursiveClauseText(ObjectCreateInfo createInfo, SqlText core)
        {
            _core = core;
            _createInfo = createInfo;
        }

        public override bool IsSingleLine(SqlConvertingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            context.ObjectCreateInfo = _createInfo;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override SqlText ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
