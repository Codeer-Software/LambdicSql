using System.Linq;
using System.Linq.Expressions;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class ConditionKeyWords
    {
        internal static SqlText ConvertLike(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }

        internal static SqlText ConvertBetween(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }

        internal static SqlText ConvertIn(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Func(LineSpace(args[0], "IN"), args[1]);
        }

        internal static SqlText ConvertExists(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause("EXISTS", args[0]);
        }

        internal static SqlText ConvertAll(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var args = methods[0].Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }
    }

    class DisableBracketsText : SqlText
    {
        SqlText _core;

        internal DisableBracketsText(SqlText core)
        {
            _core = core;
        }

        public override bool IsSingleLine(SqlConvertingContext context) => _core.IsSingleLine(context);

        public override bool IsEmpty => _core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            return _core.ToString(false, indent, context);
        }

        public override SqlText ConcatAround(string front, string back) => this;

        public override SqlText ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
