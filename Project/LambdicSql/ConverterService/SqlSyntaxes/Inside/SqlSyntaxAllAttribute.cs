using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxAllAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        internal class DisableBracketsText : Sentence
        {
            Sentence _core;

            internal DisableBracketsText(Sentence core)
            {
                _core = core;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override Sentence ConcatAround(string front, string back) => this;

            public override Sentence ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override Sentence ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override Sentence Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
