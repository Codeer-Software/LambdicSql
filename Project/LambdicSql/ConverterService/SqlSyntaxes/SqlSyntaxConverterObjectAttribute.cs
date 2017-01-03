using LambdicSql.SqlBuilder.Sentences;
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
        public abstract Sentence Convert(object obj);
    }
}
