using LambdicSql.QueryBase;
using System;

namespace LambdicSql.Inside
{
    class SqlResultDefault : ISqlResult
    {
        public bool GetBoolean(int index) => default(bool);
        public byte GetByte(int index) => default(byte);
        public DateTime GetDateTime(int index) => default(DateTime);
        public decimal GetDecimal(int index) => default(decimal);
        public double GetDouble(int index) => default(double);
        public short GetInt16(int index) => default(short);
        public int GetInt32(int index) => default(int);
        public long GetInt64(int index) => default(long);
        public float GetSingle(int index) => default(float);
        public string GetString(int index) => default(string);
    }
}
