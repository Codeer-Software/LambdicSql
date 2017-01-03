using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxAllAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        internal class DisableBracketsText : BuildingParts
        {
            BuildingParts _core;

            internal DisableBracketsText(BuildingParts core)
            {
                _core = core;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override BuildingParts ConcatAround(string front, string back) => this;

            public override BuildingParts ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override BuildingParts ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override BuildingParts Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
