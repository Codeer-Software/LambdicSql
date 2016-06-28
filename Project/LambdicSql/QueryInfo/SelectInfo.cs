using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectInfo
    {
        List<SelectElementInfo> _elements = new List<SelectElementInfo>();

        public SelectElementInfo[] GetElements() => _elements.ToArray();

        internal void Add(SelectElementInfo element)
        {
            _elements.Add(element);
        }
    }
}
