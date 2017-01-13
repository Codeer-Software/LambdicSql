using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Inside.SymbolConverters
{
    class FromConverterAttribute : MethodConverterAttribute
    {
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = ConvertTable(converter, expression.Arguments[startIndex]);
            return Clause("FROM", table);
        }

        internal static Code ConvertTable(ExpressionConverter decoder, Expression exp)
        {
            //where query, write tables side by side.
            var arry = exp as NewArrayExpression;
            if (arry != null) return Arguments(arry.Expressions.Select(e => ConvertTable(decoder, e)).ToArray());

            var table = decoder.Convert(exp);

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
                if (typeof(Sql).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }
            return null;
        }
    }
}
