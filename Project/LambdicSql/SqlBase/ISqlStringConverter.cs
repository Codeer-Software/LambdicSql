﻿namespace LambdicSql.SqlBase
{
    public interface ISqlStringConverter
    {
        DecodeContext Context { get; }
        string ToString(object obj);
    }
}