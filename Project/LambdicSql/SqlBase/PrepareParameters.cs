using LambdicSql.Inside;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LambdicSql.SqlBase
{
    public class PrepareParameters
    {
        int _count;
        Dictionary<string, DecodingParameterInfo> _parameters = new Dictionary<string, DecodingParameterInfo>();
        string _prefix;

        public PrepareParameters(string prefix)
        {
            _prefix = prefix;
        }

        internal string Push(object obj, string nameSrc = null, int? metadataToken = null, DbParam param = null)
        {
            if (string.IsNullOrEmpty(nameSrc)) nameSrc = "p_" + _count++;

            nameSrc = nameSrc.Replace(".", "_");
            var name = _prefix + nameSrc;
            DecodingParameterInfo val;
            if (_parameters.TryGetValue(name, out val))
            {
                //not be same direct value. 
                if (metadataToken != null && metadataToken == val.MetadataToken)
                {
                    return name;
                }
                while (true)
                {
                    var nameCheck = _prefix + nameSrc;
                    if (_parameters.TryGetValue(nameCheck, out val))
                    {
                        //not be same direct value. 
                        if (metadataToken != null && metadataToken == val.MetadataToken)
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
            if (param == null) param = new DbParam();
            param.Value = obj;
            _parameters.Add(name, new DecodingParameterInfo() { MetadataToken = metadataToken, Detail = param });
            return name;
        }

        internal void SetDbParam(string key, DbParam param)
        {
            param.Value = _parameters[key].Detail.Value;
            _parameters[key].Detail = param;
        }

        public Dictionary<string, object> GetParams()
            => _parameters.ToDictionary(e => e.Key, e => e.Value.Detail.Value);

        public Dictionary<string, DbParam> GetDbParams()
            => _parameters.ToDictionary(e => e.Key, e => e.Value.Detail);

        public bool TryGetParam(string name, out object leftObj)
        {
            leftObj = null;
            DecodingParameterInfo val;
            if (_parameters.TryGetValue(name, out val))
            {
                leftObj = val.Detail.Value;
                return true;
            }
            return false;
        }

        public void Remove(string name)
            => _parameters.Remove(name);

        public string ResolvePrepare(string key)
        {
            DecodingParameterInfo val;
            if (!_parameters.TryGetValue(key, out val))
            {
                return key;
            }
            _parameters.Remove(key);
            return val.Detail.Value.ToString();
        }

        public void ChangeObject(string key, object value)
            => _parameters[key].Detail.Value = value;
    }
}
