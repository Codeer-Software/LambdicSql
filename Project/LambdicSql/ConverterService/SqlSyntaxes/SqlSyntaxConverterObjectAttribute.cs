using LambdicSql.SqlBuilder.Parts;
using System;

namespace LambdicSql.ConverterService.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxConverterObjectAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract BuildingParts Convert(object obj);
    }
}
