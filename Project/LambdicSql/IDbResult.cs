using System;

namespace LambdicSql
{
    public interface IDbResult
    {
        string GetString(int index);
        bool GetBoolean(int index);
        byte GetByte(int index);
        short GetInt16(int index);
        int GetInt32(int index);
        long GetInt64(int index);
        float GetSingle(int index);
        double GetDouble(int index);
        decimal GetDecimal(int index);
        DateTime GetDateTime(int index);
    }
}
