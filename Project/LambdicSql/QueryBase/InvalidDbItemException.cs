using System;

namespace LambdicSql.QueryBase
{
    public class InvalidDbItemException : Exception
    {
        public InvalidDbItemException(string message) : base(message) { }
    }
}
