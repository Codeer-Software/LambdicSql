using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CodeParts;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;
using LambdicSql.MultiplatformCompatibe;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// Converter for FROM clause conversion.
    /// </summary>
    public class FromConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = ConvertTable(converter, expression.Arguments[startIndex]);
            if (table.IsEmpty) return string.Empty.ToCode();
            return Clause("FROM".ToCode(), table);
        }

        internal static ICode ConvertTable(ExpressionConverter decoder, Expression exp)
        {
            //where query, write tables side by side.
            var arry = exp as NewArrayExpression;
            if (arry != null)
            {
                var multiTables = new ICode[arry.Expressions.Count];
                for (int i = 0; i < multiTables.Length; i++)
                {
                    multiTables[i] = ConvertTable(decoder, arry.Expressions[i]);
                }
                return Arguments(multiTables);
            }

            var table = decoder.ConvertToCode(exp);
            if (table.IsEmpty) return string.Empty.ToCode();

            //sub query.
            var body = GetSubQuery(exp);
            if (body != null) return new SubQueryAndNameCode(body, table);

            return table;
        }

        internal static string GetSubQuery(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(Sql).IsAssignableFromEx(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }
            return null;
        }
    }
}
