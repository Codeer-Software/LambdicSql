using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class SelectClause
    {
        public Expression Define { get; internal set; }

        List<SelectElement> _elements = new List<SelectElement>();
        public SelectElement[] GetElements() => _elements.ToArray();

        internal void Add(SelectElement element)
        {
            _elements.Add(element);
        }
    }
}
