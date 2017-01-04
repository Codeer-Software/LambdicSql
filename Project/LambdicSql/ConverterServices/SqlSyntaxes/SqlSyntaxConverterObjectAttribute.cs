using LambdicSql.BuilderServices.Parts;
using System;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute for object.
    /// </summary>
    public abstract class SqlSyntaxConverterObjectAttribute : Attribute
    {
        /// <summary>
        /// Convert object to building parts.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>BuildingParts.</returns>
        public abstract BuildingParts Convert(object obj);
    }
}
