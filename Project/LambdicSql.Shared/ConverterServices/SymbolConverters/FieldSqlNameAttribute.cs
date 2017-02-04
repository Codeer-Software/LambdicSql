using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldSqlNameAttribute : Attribute
    {
        internal string Name { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name.</param>
        public FieldSqlNameAttribute(string name)
        {
            Name = name;
        }
    }
}
