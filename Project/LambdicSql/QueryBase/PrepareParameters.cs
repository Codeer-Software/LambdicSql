using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryBase
{
    class PrepareParameter
    {
        public int? MetadataToken { get; set; }
        public object Value { get; set; }
    }

    public class PrepareParameters
    {
        int _count;
        Dictionary<string, PrepareParameter> _parameters = new Dictionary<string, PrepareParameter>();

        public string Push(object obj)
        {
            var name = "p_" + _count++;
            return Push(name, null, obj);
        }

        internal string Push(string nameSrc, int? metadataToken, object obj)
        {
            nameSrc = nameSrc.Replace(".", "_");
            var name = "@" + nameSrc;
            PrepareParameter val;
            if (_parameters.TryGetValue(name, out val))
            {
                //not be same direct value. 
                if (metadataToken == val.MetadataToken)
                {
                    return name;
                }
                while (true)
                {
                    var nameCheck = "@" + nameSrc;
                    if (_parameters.TryGetValue(nameCheck, out val))
                    {
                        //not be same direct value. 
                        if (metadataToken == val.MetadataToken)
                        {
                            return nameCheck;
                        }
                    }
                    else
                    {
                        name = nameCheck;
                        break;
                    }
                    nameSrc += "_";
                }
            }
            _parameters.Add(name, new PrepareParameter() { Value = obj, MetadataToken = metadataToken });
            return name;
        }

        public Dictionary<string, object> GetParameters()
            => _parameters.ToDictionary(e => e.Key, e => e.Value.Value);

        public bool TryGetParam(string name, out object leftObj)
        {
            leftObj = null;
            PrepareParameter val;
            if (_parameters.TryGetValue(name, out val))
            {
                leftObj = val.Value;
                return true;
            }
            return false;
        }

        public void Remove(string name)
            => _parameters.Remove(name);

        public string ResolvePrepare(string key)
        {
            PrepareParameter val;
            if (!_parameters.TryGetValue(key, out val))
            {
                return key;
            }
            _parameters.Remove(key);
            return val.Value.ToString();
        }
    }
}
