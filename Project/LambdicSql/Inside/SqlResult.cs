using LambdicSql.QueryBase;
using System;
using System.Data;

namespace LambdicSql.Inside
{
    class SqlResult : ISqlResult
    {
        IDataReader _reader;

        public SqlResult(IDataReader reader)
        {
            _reader = reader;
        }

        public string GetString(int index)
        {
            if (OutOfRange(index)) return default(string);
            var data = _reader[index];
            return data == null ? default(string) : data.ToString();
        }

        public bool GetBoolean(int index)
        {
            if (OutOfRange(index)) return default(bool);
            var data = _reader[index];
            return data == null ? default(bool) :
                   data is bool ? (bool)data :
                   bool.Parse(data.ToString());
        }

        public bool? GetBooleanNullable(int index)
        {
            if (OutOfRange(index)) return default(bool?);
            var data = _reader[index];
            return data == null ? default(bool?) :
                   data is bool ? (bool)data :
                   bool.Parse(data.ToString());
        }

        public byte GetByte(int index)
        {
            if (OutOfRange(index)) return default(byte);
            var data = _reader[index];
            return data == null ? default(byte) :
                   data is byte ? (byte)data :
                   byte.Parse(data.ToString());
        }

        public byte? GetByteNullable(int index)
        {
            if (OutOfRange(index)) return default(byte?);
            var data = _reader[index];
            return data == null ? default(byte?) :
                   data is byte ? (byte)data :
                   byte.Parse(data.ToString());
        }

        public short GetInt16(int index)
        {
            if (OutOfRange(index)) return default(short);
            var data = _reader[index];
            return data == null ? default(short) :
                   data is short ? (short)data :
                   short.Parse(data.ToString());
        }

        public short? GetInt16Nullable(int index)
        {
            if (OutOfRange(index)) return default(short?);
            var data = _reader[index];
            return data == null ? default(short?) :
                   data is short ? (short)data :
                   short.Parse(data.ToString());
        }

        public int GetInt32(int index)
        {
            if (OutOfRange(index)) return default(int);
            var data = _reader[index];
            return data == null ? default(int) :
                   data is int ? (int)data :
                   int.Parse(data.ToString());
        }

        public int? GetInt32Nullable(int index)
        {
            if (OutOfRange(index)) return default(int?);
            var data = _reader[index];
            return data == null ? default(int?) :
                   data is int ? (int)data :
                   int.Parse(data.ToString());
        }

        public long GetInt64(int index)
        {
            if (OutOfRange(index)) return default(long);
            var data = _reader[index];
            return data == null ? default(long) :
                   data is long ? (long)data :
                   long.Parse(data.ToString());
        }

        public long? GetInt64Nullable(int index)
        {
            if (OutOfRange(index)) return default(long?);
            var data = _reader[index];
            return data == null ? default(long?) :
                   data is long ? (long)data :
                   long.Parse(data.ToString());
        }

        public float GetSingle(int index)
        {
            if (OutOfRange(index)) return default(float);
            var data = _reader[index];
            return data == null ? default(float) :
                   data is float ? (float)data :
                   float.Parse(data.ToString());
        }


        public float? GetSingleNullable(int index)
        {
            if (OutOfRange(index)) return default(float?);
            var data = _reader[index];
            return data == null ? default(float?) :
                   data is float ? (float)data :
                   float.Parse(data.ToString());
        }

        public double GetDouble(int index)
        {
            if (OutOfRange(index)) return default(double);
            var data = _reader[index];
            return data == null ? default(double) :
                   data is double ? (double)data :
                   double.Parse(data.ToString());
        }

        public double? GetDoubleNullable(int index)
        {
            if (OutOfRange(index)) return default(double?);
            var data = _reader[index];
            return data == null ? default(double?) :
                   data is double ? (double)data :
                   double.Parse(data.ToString());
        }

        public decimal GetDecimal(int index)
        {
            if (OutOfRange(index)) return default(decimal);
            var data = _reader[index];
            return data == null ? default(decimal) :
                   data is decimal ? (decimal)data :
                   decimal.Parse(data.ToString());
        }

        public decimal? GetDecimalNullable(int index)
        {
            if (OutOfRange(index)) return default(decimal?);
            var data = _reader[index];
            return data == null ? default(decimal?) :
                   data is decimal ? (decimal)data :
                   decimal.Parse(data.ToString());
        }

        public DateTime GetDateTime(int index)
        {
            if (OutOfRange(index)) return default(DateTime);
            var data = _reader[index];
            return data == null ? default(DateTime) :
                   data is DateTime ? (DateTime)data :
                   DateTime.Parse(data.ToString());
        }

        public DateTime? GetDateTimeNullable(int index)
        {
            if (OutOfRange(index)) return default(DateTime?);
            var data = _reader[index];
            return data == null ? default(DateTime?) :
                   data is DateTime ? (DateTime)data :
                   DateTime.Parse(data.ToString());
        }

        bool OutOfRange(int index) => index < 0 || _reader.FieldCount <= index;
    }
}
