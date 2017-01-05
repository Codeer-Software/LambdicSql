using LambdicSql.BuilderServices.Parts;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for object.
    /// </summary>
    public abstract class SymbolConverterObjectAttribute : Attribute
    {
        /// <summary>
        /// Convert object to building parts.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>BuildingParts.</returns>
        public abstract BuildingParts Convert(object obj);
    }
}
