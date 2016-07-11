using LambdicSql.QueryBase;
using LambdicSql.SqlServer;
using System;
using System.IO;

namespace Performance
{
    static class TestEnvironment
    {
        internal static string ConnectionString => File.ReadAllText(FindNearFile("db.txt")).Trim();
        internal static IDbAdapter Adapter => new SqlServerAdapter(ConnectionString);

        static string FindNearFile(string fileName)
        {
            var path = typeof(TestEnvironment).Assembly.Location;
            while (true)
            {
                var filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                path = Path.GetDirectoryName(path);
            }
            throw new NotSupportedException();
        }
    }
}
