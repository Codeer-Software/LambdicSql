using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.Inside.CodeParts;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;
using System.Linq.Expressions;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// Converter for SELECT clause conversion.
    /// </summary>
    public class SelectConverterAttribute : MethodConverterAttribute
    {
        static readonly ICode SelectClause = "SELECT".ToCode();
        static readonly ICode AsClause = "AS".ToCode();

        /// <summary>
        /// Set when using names other than Select.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            //ALL, DISTINCT, TOP
            int expressionIndex = expression.SkipMethodChain(0);
            var selectParts = new ICode[expression.Arguments.Count - expressionIndex];
            selectParts[0] = string.IsNullOrEmpty(Name) ? SelectClause : Name.ToCode();
            for (int i = 0; i < selectParts.Length - 1; i++, expressionIndex++)
            {
                selectParts[i + 1] = converter.ConvertToCode(expression.Arguments[expressionIndex]);
            }

            var select = LineSpace(selectParts);

            //select elemnts.
            var selectTargets = expression.Arguments[expression.Arguments.Count - 1];

            //*
            if (typeof(AsteriskElement).IsAssignableFromEx(selectTargets.Type))
            {
                select.Add("*".ToCode());
                return new SelectClauseCode(select);
            }

            //new []{ a, b, c} recursive.
            else if (selectTargets.Type.IsArray)
            {
                var newArrayExp = selectTargets as NewArrayExpression;
                if (newArrayExp != null)
                {
                    var array = new ICode[newArrayExp.Expressions.Count];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = converter.ConvertToCode(newArrayExp.Expressions[i]);
                    }

                    var coreCode = new VCode(select, new VCode(array) { Indent = 1, Separator = "," });
                    return new SelectClauseCode(coreCode);
                }
            }

            //new { item = db.tbl.column }
            {
                var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
                var elements = new ICode[createInfo.Members.Length];
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i] = ConvertSelectedElement(converter, createInfo.Members[i]);
                }

                var coreCode = new VCode(select, new VCode(elements) { Indent = 1, Separator = "," });
                return new SelectClauseCode(coreCode);
            }
        }

        static ICode ConvertSelectedElement(ExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.ConvertToCode(element.Expression);

            //normal select.
            var exp = converter.ConvertToCode(element.Expression);
            return exp.IsEmpty ? exp : LineSpace(exp, AsClause, element.Name.ToCode());
        }
    }
}
