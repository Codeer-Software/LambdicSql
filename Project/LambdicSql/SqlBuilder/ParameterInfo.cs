using LambdicSql.ConverterService.Inside;
using LambdicSql.Inside;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.SqlBuilder
{
    /// <summary>
    /// Parameter info.
    /// </summary>
    public class ParameterInfo
    {
        int _count;
        Dictionary<string, DecodingParameterInfo> _parameters = new Dictionary<string, DecodingParameterInfo>();
        string _prefix;

        internal ParameterInfo(string prefix)
        {
            _prefix = prefix;
        }
        /// <summary>
        /// Get parameters.
        /// It's dictionary of name and parameter.
        /// </summary>
        /// <returns>parameters.</returns>
        public Dictionary<string, DbParam> GetDbParams()
            => _parameters.ToDictionary(e => e.Key, e => e.Value.Detail);

        internal string Push(object obj, string nameSrc = null, MetaId metadataToken = null, DbParam param = null)
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
    }
}
