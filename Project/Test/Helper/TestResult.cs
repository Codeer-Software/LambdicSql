﻿using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class TestResult : ISqlResult
    {
        public Dictionary<string, object> Data { get; } = new Dictionary<string, object>();
        public object this[string key] { get { return Data[key]; } set { Data[key] = value; } }

        public string GetString(int index) => (string)Data.Values.ToList()[index];
        public string GetStringNullable(int index) => (string)Data.Values.ToList()[index];
        public bool GetBoolean(int index) => (bool)Data.Values.ToList()[index];
        public bool? GetBooleanNullable(int index) => (bool)Data.Values.ToList()[index];
        public byte GetByte(int index) => (byte)Data.Values.ToList()[index];
        public byte? GetByteNullable(int index) => (byte)Data.Values.ToList()[index];
        public short GetInt16(int index) => (short)Data.Values.ToList()[index];
        public short? GetInt16Nullable(int index) => (short)Data.Values.ToList()[index];
        public int GetInt32(int index) => (int)Data.Values.ToList()[index];
        public int? GetInt32Nullable(int index) => (int)Data.Values.ToList()[index];
        public long GetInt64(int index) => (long)Data.Values.ToList()[index];
        public long? GetInt64Nullable(int index) => (long)Data.Values.ToList()[index];
        public float GetSingle(int index) => (float)Data.Values.ToList()[index];
        public float? GetSingleNullable(int index) => (float)Data.Values.ToList()[index];
        public double GetDouble(int index) => (double)Data.Values.ToList()[index];
        public double? GetDoubleNullable(int index) => (double)Data.Values.ToList()[index];
        public decimal GetDecimal(int index) => (decimal)Data.Values.ToList()[index];
        public decimal? GetDecimalNullable(int index) => (decimal)Data.Values.ToList()[index];
        public DateTime GetDateTime(int index) => (DateTime)Data.Values.ToList()[index];
        public DateTime? GetDateTimeNullable(int index) => (DateTime)Data.Values.ToList()[index];

        internal T Create<T>(IQuery<T, T> query)
            where T : class
        {
            var info = query as ISelectedQuery<T>;
            return info.Create(this);
        }
    }
}
