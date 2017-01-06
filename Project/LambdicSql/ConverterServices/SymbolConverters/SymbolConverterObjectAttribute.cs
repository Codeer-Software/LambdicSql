using LambdicSql.BuilderServices.Code;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for object.
    /// </summary>
    public abstract class SymbolConverterObjectAttribute : Attribute
    {
        /// <summary>
        /// Convert object to code parts.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Parts.</returns>
        public abstract Parts Convert(object obj);
    }
}
