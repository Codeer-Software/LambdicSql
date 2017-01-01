using LambdicSql.SqlBuilder.ExpressionElements;
using System;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes
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
