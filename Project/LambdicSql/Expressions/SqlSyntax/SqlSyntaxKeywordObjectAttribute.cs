using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Expressions.SqlSyntax
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordObjectAttribute : SqlSyntaxObjectAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(object obj)
            => obj == null ? string.Empty :
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;
    }
}
