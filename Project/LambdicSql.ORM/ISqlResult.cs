using System;

namespace LambdicSql.SqlBase
{
    public interface ISqlResult
    {
        string GetString(int index);
        bool GetBoolean(int index);
        bool? GetBooleanNullable(int index);
        byte GetByte(int index);
        byte? GetByteNullable(int index);
        short GetInt16(int index);
        short? GetInt16Nullable(int index);
        int GetInt32(int index);
        int? GetInt32Nullable(int index);
        long GetInt64(int index);
        long? GetInt64Nullable(int index);
        float GetSingle(int index);
        float? GetSingleNullable(int index);
        double GetDouble(int index);
        double? GetDoubleNullable(int index);
        decimal GetDecimal(int index);
        decimal? GetDecimalNullable(int index);
        DateTime GetDateTime(int index);
        DateTime? GetDateTimeNullable(int index);
    }
}
