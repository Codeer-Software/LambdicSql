/*
using System.Collections.Generic;
using System.Data;

namespace Dapper
{
    static class FakeDapper
    {
        public static IEnumerable<T> Query<T>(this IDbConnection _connection, string sql, Dictionary<string, object> param = null) => new List<T>();
        public static int Execute(this IDbConnection _connection, string sql, Dictionary<string, object> param) => 0;
    }
}
*/