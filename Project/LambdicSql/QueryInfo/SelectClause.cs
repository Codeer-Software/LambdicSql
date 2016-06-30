using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryInfo
{
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
}
