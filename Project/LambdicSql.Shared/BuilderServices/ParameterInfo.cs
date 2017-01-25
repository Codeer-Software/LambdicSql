using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Parameter info.
    /// </summary>
    public class ParameterInfo
    {
        class DecodingParameterInfo
        {
            internal MetaId MetadataToken { get; set; }
            internal IDbParam Detail { get; set; }
        }

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
        public Dictionary<string, IDbParam> GetDbParams()
            => _parameters.ToDictionary(e => e.Key, e => e.Value.Detail);

        internal string Push(object obj, string nameSrc = null, MetaId metadataToken = null, IDbParam param = null)
        {
            if (string.IsNullOrEmpty(nameSrc)) nameSrc = "p_" + _count++;
            else nameSrc = nameSrc.Replace(".", "_");

            var name = _prefix + nameSrc;

            DecodingParameterInfo val;
            if (_parameters.TryGetValue(name, out val))
            {
                //find same metatoken object.
                if (metadataToken != null && metadataToken == val.MetadataToken) return name;

                //make unique name.
                name = MakeUniqueName(nameSrc);
            }

            //register.
            if (param == null) param = new DbParamValueOnly() { Value = obj };
            _parameters.Add(name, new DecodingParameterInfo() { MetadataToken = metadataToken, Detail = param });

            return name;
        }

        string MakeUniqueName(string nameSrc)
        {
            while (true)
            {
                var nameCheck = _prefix + nameSrc;
                DecodingParameterInfo val;
                if (!_parameters.TryGetValue(nameCheck, out val))
                {
                    return nameCheck;
                }
                nameSrc += "_";
            }
        }
    }
}
