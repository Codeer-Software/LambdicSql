using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectInfo
    {
        List<SelectElementInfo> _elements = new List<SelectElementInfo>();

        public IReadOnlyList<SelectElementInfo> Elements => _elements;

        internal void Add(SelectElementInfo element)
        {
            _elements.Add(element);
        }
    }
}
