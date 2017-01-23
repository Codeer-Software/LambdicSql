using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// Extensions for BuildedSql.
    /// </summary>
    public static class BuildedSqlExtensionsPCL
    {
        /// <summary>
        /// Get parameters.
        /// </summary>
        /// <param name="sql">BuildedSql.</param>
        /// <returns>Parameters.</returns>
        public static object[] GetParamValues(this BuildedSql sql)
        {
            var tokens = sql.Text.Split(new char[] { ' ', ',', '(', ')', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return sql.GetParams(e => e.Value).Select(e => new { Index = tokens.IndexOfCheck(e.Key), Value = e.Value }).OrderBy(e => e.Index).Select(e => e.Value).ToArray();
        }

        public static int IndexOfCheck(this List<string> target, string value)
        {
            var index = target.IndexOf(value);
            if (index == -1) throw new NotSupportedException();
            return index;
        }
    }
}
