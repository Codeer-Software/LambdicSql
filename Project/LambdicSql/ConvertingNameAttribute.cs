using System;

namespace LambdicSql
{
    /// <summary>
    /// Give members a name to be used for conversion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConvertingNameAttribute : Attribute
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
    }
}
