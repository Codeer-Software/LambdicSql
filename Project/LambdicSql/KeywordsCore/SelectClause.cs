using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SelectClause
    {
        public static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var index = method.SqlSyntaxMethodArgumentAdjuster();

            Expression define = null;
            AggregatePredicate? aggregatePredicate = null;
            if (method.Arguments[index(0)].Type == typeof(AggregatePredicate))
            {
                aggregatePredicate = (AggregatePredicate)((ConstantExpression)method.Arguments[index(0)]).Value;
                define = method.Arguments[index(1)];
            }
            else
            {
                define = method.Arguments[index(0)];
            }

            var select = ObjectCreateAnalyzer.MakeSelectInfo(define);

            if (converter.Context.SelectClauseInfo == null)
            {
                converter.Context.SelectClauseInfo = select;
            }
            var text = ToString(GetPredicate(aggregatePredicate), select.Elements, converter);
            if (method.Method.Name == nameof(Sql.SelectFrom))
            {
                text = text + Environment.NewLine + "FROM " + converter.ToString(method.Arguments[index(0)]);
            }
            return Environment.NewLine + text;
        }

        static string ToString(string _predicate, ObjectCreateMemberElement[] _elements, ISqlStringConverter decoder)
            => "SELECT" + _predicate + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e => ToString(decoder, e)).ToArray());

        static string ToString(ISqlStringConverter decoder, ObjectCreateMemberElement element)
            => element.Expression == null ? element.Name : decoder.ToString(element.Expression) + " AS \"" + element.Name + "\"";

        static string GetPredicate(AggregatePredicate? aggregatePredicate)
        {
            if (aggregatePredicate == null)
            {
                return string.Empty;
            }
            switch (aggregatePredicate)
            {
                case AggregatePredicate.All:
                    return " ALL";
                case AggregatePredicate.Distinct:
                    return " DISTINCT";
            }
            return string.Empty;
        }
    }
}
