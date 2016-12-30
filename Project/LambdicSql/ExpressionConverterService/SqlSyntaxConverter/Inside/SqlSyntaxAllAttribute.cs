using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
{
    class SqlSyntaxAllAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        internal class DisableBracketsText : ExpressionElement
        {
            ExpressionElement _core;

            internal DisableBracketsText(ExpressionElement core)
            {
                _core = core;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back) => this;

            public override ExpressionElement ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override ExpressionElement ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
