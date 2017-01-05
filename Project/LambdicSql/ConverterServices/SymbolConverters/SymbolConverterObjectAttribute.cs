using LambdicSql.BuilderServices.Syntaxes;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for object.
    /// </summary>
    public abstract class SymbolConverterObjectAttribute : Attribute
    {
        /// <summary>
        /// Convert object to syntax.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Syntax.</returns>
        public abstract Syntax Convert(object obj);
    }
}
