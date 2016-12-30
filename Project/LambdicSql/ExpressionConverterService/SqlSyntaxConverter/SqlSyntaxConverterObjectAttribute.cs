using LambdicSql.SqlBase.TextParts;
using System;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter
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
        public abstract ExpressionElement Convert(object obj);
    }
}
