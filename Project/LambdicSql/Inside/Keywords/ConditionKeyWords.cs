using System.Linq;
using System.Linq.Expressions;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static ExpressionElement ConvertLike(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }

        internal static ExpressionElement ConvertBetween(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }

        internal static ExpressionElement ConvertIn(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Func(LineSpace(args[0], "IN"), args[1]);
        }

        internal static ExpressionElement ConvertExists(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause("EXISTS", args[0]);
        }

        internal static ExpressionElement ConvertAll(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }
    }

    class DisableBracketsText : ExpressionElement
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
