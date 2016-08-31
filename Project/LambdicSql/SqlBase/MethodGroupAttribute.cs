using System;

namespace LambdicSql.SqlBase
{
    public class MethodGroupAttribute : Attribute
    {
        public string GroupName { get; }
        public MethodGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}
