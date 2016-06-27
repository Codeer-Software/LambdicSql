using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectInfo
    {
        List<SelectElementInfo> _elements = new List<SelectElementInfo>();

        public List<SelectElementInfo> Elements => _elements;

        internal void Add(SelectElementInfo element)
        {
            _elements.Add(element);
        }
    }
}
