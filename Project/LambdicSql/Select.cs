using LambdicSql.Inside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class SelectElement
    {
        public string Name { get; }
        public Expression Expression { get; }

        public SelectElement(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }

    public class SelectClause : IClause
    {
        List<SelectElement> _elements = new List<SelectElement>();

        public SelectElement[] GetElements() => _elements.ToArray();

        internal void Add(SelectElement element)
        {
            _elements.Add(element);
        }

        public IClause Clone() => this;

        public string ToString(IExpressionDecoder decoder)
            => "SELECT" + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e => ToString(decoder, e)).ToArray());

        string ToString(IExpressionDecoder decoder, SelectElement element)
            => element.Expression == null ? element.Name : decoder.ToString(element.Expression) + " AS \"" + element.Name + "\"";
    }

    public static class SelectClauseExtensions
    {
        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, ISelectFuncs, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        static IQuery<TDB, TSelect> SelectCore<TDB, TSelect>(this IQuery<TDB, TDB> query, LambdaExpression define)
            where TDB : class
            where TSelect : class
        {
            var src = query as Query<TDB, TDB>;
            var select = SelectDefineAnalyzer.MakeSelectInfo(define.Body);

            var indexInSelect = select.GetElements().Select(e => e.Name).ToList();
            var dst = src.ConvertType(ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(name => indexInSelect.IndexOf(name), define.Body));
            dst.Select = select;
            return dst;
        }
    }

}
