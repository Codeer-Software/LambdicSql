using System.Collections.Generic;

namespace LambdicSql
{
    /// <summary>
    /// Extensions for BuildedSql.
    /// </summary>
    public static class BuildedSqlExtensions
    {
        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <param name="sql">BuildedSql.</param>
        /// <returns>Parameters.</returns>
        public static Dictionary<string, DbParam> GetParams(this BuildedSql sql)
        {
            return sql.GetParams(e =>
            {
                var dbParam = e as DbParam;
                return dbParam == null ? new DbParam { Value = e.Value } : dbParam;
            });
        }
    }
}
