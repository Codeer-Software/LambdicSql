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

        public string GetString(int index)
        {
            var data = _reader[index];
            return data == null ? default(string) : data.ToString();
        }
        public bool GetBoolean(int index)
        {
            var data = _reader[index];
            return data == null ? default(bool) :
                   data is bool ? (bool)data :
                   bool.Parse(data.ToString());
        }
        public byte GetByte(int index)
        {
            var data = _reader[index];
            return data == null ? default(byte) :
                   data is byte ? (byte)data :
                   byte.Parse(data.ToString());
        }
        public short GetInt16(int index)
        {
            var data = _reader[index];
            return data == null ? default(short) :
                   data is short ? (short)data :
                   short.Parse(data.ToString());
        }
        public int GetInt32(int index)
        {
            var data = _reader[index];
            return data == null ? default(int) :
                   data is int ? (int)data :
                   int.Parse(data.ToString());
        }
        public long GetInt64(int index)
        {
            var data = _reader[index];
            return data == null ? default(long) :
                   data is long ? (long)data :
                   long.Parse(data.ToString());
        }
        public float GetSingle(int index)
        {
            var data = _reader[index];
            return data == null ? default(float) :
                   data is float ? (float)data :
                   float.Parse(data.ToString());
        }
        public double GetDouble(int index)
        {
            var data = _reader[index];
            return data == null ? default(double) :
                   data is double ? (double)data :
                   double.Parse(data.ToString());
        }
        public decimal GetDecimal(int index)
        {
            var data = _reader[index];
            return data == null ? default(decimal) :
                   data is decimal ? (decimal)data :
                   decimal.Parse(data.ToString());
        }

        public DateTime GetDateTime(int index)
        {
            var data = _reader[index];
            return data == null ? default(DateTime) :
                   data is DateTime ? (DateTime)data :
                   DateTime.Parse(data.ToString());
        }
    }
}
