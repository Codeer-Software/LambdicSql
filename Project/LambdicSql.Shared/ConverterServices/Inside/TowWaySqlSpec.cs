using System;

namespace LambdicSql.ConverterServices.Inside
{
    static class TowWaySqlSpec
    {
        internal static string ToStringFormat(string sql)
        {
            for (int i = 0; true; i++)
            {
                int changeCount = 0;
                while (true)
                {
                    var start = "/*" + i + "*/";
                    var startIndex = sql.IndexOf(start);
                    if (startIndex == -1)
                    {
                        break;
                    }
                    changeCount++;
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
                if (changeCount == 0) break;
            }
            return sql;
        }
    }
}
