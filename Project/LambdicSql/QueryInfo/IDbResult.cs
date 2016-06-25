using System;

namespace LambdicSql.QueryInfo
{
    public interface IDbResult
    {
        string GetString(string name);
        int GetInt32(string name);
        DateTime GetDateTime(string name);
    }

    //TODO must support
    /*
     * SqlBinary GetSqlBinary(int i);
       SqlBoolean GetSqlBoolean(int i);
       SqlByte GetSqlByte(int i);
       SqlBytes GetSqlBytes(int i);
       SqlChars GetSqlChars(int i);
       SqlDateTime GetSqlDateTime(int i);
       SqlDecimal GetSqlDecimal(int i);
       SqlDouble GetSqlDouble(int i);
       SqlGuid GetSqlGuid(int i);
       SqlInt16 GetSqlInt16(int i);
       SqlInt32 GetSqlInt32(int i);
       SqlInt64 GetSqlInt64(int i);
       SqlMoney GetSqlMoney(int i);
       SqlSingle GetSqlSingle(int i);
       SqlString GetSqlString(int i);
       */
}
