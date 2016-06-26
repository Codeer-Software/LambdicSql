using LambdicSql;
using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;

namespace Test
{
    class TestResult : IDbResult
    {
        public Dictionary<string, object> Data { get; } = new Dictionary<string, object>();
        public object this[string key] { get { return Data[key]; } set { Data[key] = value; } }

        public string GetString(string name) => (string)Data[name];
        public bool GetBoolean(string name) => (bool)Data[name];
        public byte GetByte(string name) => (byte)Data[name];
        public short GetInt16(string name) => (short)Data[name];
        public int GetInt32(string name) => (int)Data[name];
        public long GetInt64(string name) => (long)Data[name];
        public float GetSingle(string name) => (float)Data[name];
        public double GetDouble(string name) => (double)Data[name];
        public decimal GetDecimal(string name) => (decimal)Data[name];
        public DateTime GetDateTime(string name) => (DateTime)Data[name];

        internal T Create<T>(IQuery<T, T> query)
            where T : class
        {
            var info = query as IQueryInfo<T>;
            return info.Create(this);
        }
    }
}
