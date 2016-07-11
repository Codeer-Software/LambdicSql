using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryBase
{
    public class PrepareParameters
    {
        int _count;
        Dictionary<string, object> _parameters = new Dictionary<string, object>();

        public string Push(object param)
        {
            var name = "@p_" + _count++;
            _parameters.Add(name, param);
            return name;
        }

        public Dictionary<string, object> GetParameters()
            => _parameters.ToDictionary(e => e.Key, e => e.Value);

        public bool TryGetParam(string name, out object leftObj)
            => _parameters.TryGetValue(name, out leftObj);

        public void Remove(string name)
            => _parameters.Remove(name);
    }
}
