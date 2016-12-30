using LambdicSql.SqlBase.TextParts;
using System;

namespace LambdicSql.Expressions.SqlSyntax
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
