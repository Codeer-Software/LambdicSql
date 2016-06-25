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

        public DateTime GetDateTime(string name) => (DateTime)Data[name];
        public int GetInt32(string name) => (int)Data[name];
        public string GetString(string name) => (string)Data[name];

        internal T Create<T>(IQuery<T, T> query)
            where T : class
        {
            var info = query as IQueryInfo<T>;
            return info.Create(this);
        }
    }
}
