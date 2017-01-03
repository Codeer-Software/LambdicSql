using System;

namespace LambdicSql.ConverterService.Inside
{
    static class TowWaySqlSpec
    {
        internal static string ToStringFormat(string sql)
        {
            for (int i = 0; true; i++)
            {
                var start = "/*" + i + "*/";
                var startIndex = sql.IndexOf(start);
                if (startIndex == -1)
                {
                    break;
                }
                var end = "/**/";
                var endIndex = sql.IndexOf(end, startIndex + start.Length);
                if (endIndex == -1)
                {
                    throw new NotSupportedException("Invalid 2WaySql format.");
                }

                var before = sql.Substring(0, startIndex);
                var after = sql.Substring(endIndex + end.Length);
                sql = before + "{" + i + "}" + after;
            }
            return sql;
        }
    }
}
