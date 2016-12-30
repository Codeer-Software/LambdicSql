using LambdicSql.SqlBase.TextParts;
using System;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxObjectAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(object obj);
    }
}
