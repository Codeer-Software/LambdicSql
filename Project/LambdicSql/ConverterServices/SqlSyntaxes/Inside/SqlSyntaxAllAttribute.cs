using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxAllAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        //TODO そもそも括弧つけの手法が見直せたら良い
        internal class DisableBracketsText : BuildingParts
        {
            BuildingParts _core;

            internal DisableBracketsText(BuildingParts core)
            {
                _core = core;
            }

            public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override BuildingParts ConcatAround(string front, string back) => this;

            public override BuildingParts ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override BuildingParts ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override BuildingParts Customize(IPartsCustomizer customizer) => customizer.Custom(this);
        }
    }
}
