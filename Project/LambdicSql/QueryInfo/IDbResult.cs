using System;

namespace LambdicSql.QueryInfo
{
    public interface IDbResult
    {
        string GetString(string name);
        bool GetBoolean(string name);
        byte GetByte(string name);
        short GetInt16(string name);
        int GetInt32(string name);
        long GetInt64(string name);
        float GetSingle(string name);
        double GetDouble(string name);
        decimal GetDecimal(string name);
        DateTime GetDateTime(string name);
    }
}
