using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using LambdicSql.SqlBuilder.Sentences.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxRecursiveAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (Sentence)e.Name).ToArray()));
        }

        class RecursiveClauseText : Sentence
        {
            Sentence _core;
            ObjectCreateInfo _createInfo;

            internal RecursiveClauseText(ObjectCreateInfo createInfo, Sentence core)
            {
                _core = core;
                _createInfo = createInfo;
            }

            public override bool IsSingleLine(SqlBuildingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                context.ObjectCreateInfo = _createInfo;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Sentence ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

            public override Sentence ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

            public override Sentence ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

            public override Sentence Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
