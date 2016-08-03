﻿using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SelectWordsExtensions
    {
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlSyntax words, AggregatePredicate predicate, TSelected selected) => null;
        public static ISqlWords<TSelected> Select<TSelected>(this ISqlSyntax words, TSelected selected) => null;
        public static ISqlWords<TSelected> SelectFrom<TSelected>(this ISqlSyntax words, TSelected selected) => null;

        public static string MethodToString(ISqlStringConverter converter, MethodCallExpression method)
        {
            Expression define = null;
            AggregatePredicate? aggregatePredicate = null;
            if (method.Arguments[1].Type == typeof(AggregatePredicate))
            {
                aggregatePredicate = (AggregatePredicate)((ConstantExpression)method.Arguments[1]).Value;
                define = method.Arguments[2];
            }
            else
            {
                define = method.Arguments[1];
            }

            var select = SelectDefineAnalyzer.MakeSelectInfo(define);

            if (converter.DbInfo.SelectClause == null)
            {
                converter.DbInfo.SelectClause = select;
                converter.DbInfo.SelectClause.Define = define;
            }
            var text = ToString(GetPredicate(aggregatePredicate), select.GetElements(), converter);
            if (method.Method.Name == nameof(SelectFrom))
            {
                text = text + Environment.NewLine + "FROM " + converter.ToString(method.Arguments[1]);
            }
            return Environment.NewLine + text;
        }

        static string ToString(string _predicate, SelectElement[] _elements, ISqlStringConverter decoder)
            => "SELECT" + _predicate + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e => ToString(decoder, e)).ToArray());

        static string ToString(ISqlStringConverter decoder, SelectElement element)
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
