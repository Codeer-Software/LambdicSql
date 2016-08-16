using LambdicSql.Inside;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBase
{
    public class PrepareParameters
    {
        int _count;
        Dictionary<string, DecodingParameterInfo> _parameters = new Dictionary<string, DecodingParameterInfo>();
        
        internal string Push(object obj, string nameSrc = null, int? metadataToken = null, DbParam param = null)
        {
            if (string.IsNullOrEmpty(nameSrc)) nameSrc = "p_" + _count++;

            nameSrc = nameSrc.Replace(".", "_");
            var name = "@" + nameSrc;
            DecodingParameterInfo val;
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
            if (param == null)
            {
                param = new DbParam(obj);
            }
            _parameters.Add(name, new DecodingParameterInfo() { MetadataToken = metadataToken, Detail = param });
            return name;
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
    }
}
