using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Parts;
using LambdicSql.SqlBuilder.Parts.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxRecursiveAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (BuildingParts)e.Name).ToArray()));
        }

        class RecursiveClauseText : BuildingParts
        {
            BuildingParts _core;
            ObjectCreateInfo _createInfo;

            internal RecursiveClauseText(ObjectCreateInfo createInfo, BuildingParts core)
            {
                _core = core;
                _createInfo = createInfo;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                return _core.ToString(isTopLevel, indent, context);
            }

            public override BuildingParts ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

            public override BuildingParts ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

            public override BuildingParts ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

            public override BuildingParts Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
