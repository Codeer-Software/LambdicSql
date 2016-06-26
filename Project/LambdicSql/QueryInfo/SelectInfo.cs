using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectInfo
    {
        Dictionary<string, ISelectElementInfo> _aliasElements = new Dictionary<string, ISelectElementInfo>();

        public IReadOnlyDictionary<string, ISelectElementInfo> AliasElements => _aliasElements;

        internal void Add(string name, ISelectElementInfo selectElementInfo)
        {
            _aliasElements.Add(name, selectElementInfo);
        }
    }
}
