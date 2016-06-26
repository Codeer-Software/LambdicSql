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

        public string GetString(string name)
        {
            var data = _reader[name];
            return data == null ? default(string) : data.ToString();
        }
        public bool GetBoolean(string name)
        {
            var data = _reader[name];
            return data == null ? default(bool) :
                   data is bool ? (bool)data :
                   bool.Parse(data.ToString());
        }
        public byte GetByte(string name)
        {
            var data = _reader[name];
            return data == null ? default(byte) :
                   data is byte ? (byte)data :
                   byte.Parse(data.ToString());
        }
        public short GetInt16(string name)
        {
            var data = _reader[name];
            return data == null ? default(short) :
                   data is short ? (short)data :
                   short.Parse(data.ToString());
        }
        public int GetInt32(string name)
        {
            var data = _reader[name];
            return data == null ? default(int) :
                   data is int ? (int)data :
                   int.Parse(data.ToString());
        }
        public long GetInt64(string name)
        {
            var data = _reader[name];
            return data == null ? default(long) :
                   data is long ? (long)data :
                   long.Parse(data.ToString());
        }
        public float GetSingle(string name)
        {
            var data = _reader[name];
            return data == null ? default(float) :
                   data is float ? (float)data :
                   float.Parse(data.ToString());
        }
        public double GetDouble(string name)
        {
            var data = _reader[name];
            return data == null ? default(double) :
                   data is double ? (double)data :
                   double.Parse(data.ToString());
        }
        public decimal GetDecimal(string name)
        {
            var data = _reader[name];
            return data == null ? default(decimal) :
                   data is decimal ? (decimal)data :
                   decimal.Parse(data.ToString());
        }

        public DateTime GetDateTime(string name)
        {
            var data = _reader[name];
            return data == null ? default(DateTime) :
                   data is DateTime ? (DateTime)data :
                   DateTime.Parse(data.ToString());
        }
    }
}
