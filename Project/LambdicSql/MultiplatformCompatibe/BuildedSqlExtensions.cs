using System.Collections.Generic;

namespace LambdicSql
{
    /// <summary>
    /// 
    /// </summary>
    public static class BuildedSqlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Dictionary<string, DbParam> GetParams(this BuildedSql sql)
        {
            return sql.GetParams(e =>
            {
                var dbParam = e as DbParam;
                if (dbParam == null)
                {
                    dbParam = new DbParam { Value = e.Value };
                }
                return dbParam;
            });
        }
    }
}
