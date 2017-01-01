using LambdicSql.ExpressionConverterService.Inside;
using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.ExpressionElements;
using LambdicSql.SqlBuilder.ExpressionElements.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxRecursiveAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (ExpressionElement)e.Name).ToArray()));
        }

        class RecursiveClauseText : ExpressionElement
        {
            ExpressionElement _core;
            ObjectCreateInfo _createInfo;

            internal RecursiveClauseText(ObjectCreateInfo createInfo, ExpressionElement core)
            {
                _core = core;
                _createInfo = createInfo;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                context.ObjectCreateInfo = _createInfo;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

            public override ExpressionElement ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

            public override ExpressionElement ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

            public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }
}
