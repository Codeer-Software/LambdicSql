using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Words
{
    public class SelectClause : IClause
    {
        public Type SelectedType { get; internal set; }//TODO
        public Expression Define { get; internal set; }

        List<SelectElement> _elements = new List<SelectElement>();
        string _predicate = string.Empty;
        public SelectElement[] GetElements() => _elements.ToArray();

        internal void Add(SelectElement element)
        {
            _elements.Add(element);
        }

        public IClause Clone() => this;

        public string ToString(ISqlStringConverter decoder)
            => "SELECT" + _predicate + Environment.NewLine + "\t" +
            string.Join("," + Environment.NewLine + "\t", _elements.Select(e => ToString(decoder, e)).ToArray());

        string ToString(ISqlStringConverter decoder, SelectElement element)
            => element.Expression == null ? element.Name : decoder.ToString(element.Expression) + " AS \"" + element.Name + "\"";

        internal void SetPredicate(AggregatePredicate? aggregatePredicate)
        {
            if (aggregatePredicate == null)
            {
                return;
            }
            switch (aggregatePredicate)
            {
                case AggregatePredicate.All:
                    _predicate = " ALL";
                    break;
                case AggregatePredicate.Distinct:
                    _predicate = " DISTINCT";
                    break;
            }
        }
    }
}
