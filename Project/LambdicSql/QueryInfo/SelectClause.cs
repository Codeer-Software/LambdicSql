using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectClause
    {
        List<SelectElement> _elements = new List<SelectElement>();

        public SelectElement[] GetElements() => _elements.ToArray();

        internal void Add(SelectElement element)
        {
            _elements.Add(element);
        }
    }
}
