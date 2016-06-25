using LambdicSql.QueryInfo;
using System;
using System.Data.SqlClient;

namespace LambdicSql.Inside
{
    class DbResult : IDbResult
    {
        SqlDataReader _reader;

        public DbResult(SqlDataReader reader)
        {
            _reader = reader;
        }

        public DateTime GetDateTime(string name)
        {
            var data = _reader[name];
            return data == null ? default(DateTime) :
                   data is DateTime ? (DateTime)data :
                   DateTime.Parse(data.ToString());
        }

        public int GetInt32(string name)
        {
            var data = _reader[name];
            return data == null ? default(int) :
                   data is int ? (int)data :
                   int.Parse(data.ToString());
        }

        public string GetString(string name)
        {
            var data = _reader[name];
            return data == null ? default(string) : data.ToString();
        }
    }
}
