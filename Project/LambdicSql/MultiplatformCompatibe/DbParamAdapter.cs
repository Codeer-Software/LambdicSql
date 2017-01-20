using LambdicSql.ConverterServices;
using System.Collections.Generic;

namespace LambdicSql.MultiplatformCompatibe
{
    static class DbParamAdapter
    {
        public static Dictionary<string, DbParam> Adapt(this Dictionary<string, IDbParam> src)
        {
            var dst = new Dictionary<string, DbParam>();
            foreach (var e in src)
            {
                var dbParam = e.Value as DbParam;
                if (dbParam == null)
                {
                    dbParam = new DbParam { Value = e.Value.Value };
                }
                dst[e.Key] = dbParam;
            }
            return dst;
        }
    }
}
