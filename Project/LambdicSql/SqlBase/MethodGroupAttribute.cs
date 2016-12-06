using System;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// This attribute indicates that consecutive attributes belong to the same group.
    /// </summary>
    public class MethodGroupAttribute : Attribute
    {
        /// <summary>
        /// Group name.
        /// </summary>
        public string GroupName { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="groupName">Group name.</param>
        public MethodGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}
