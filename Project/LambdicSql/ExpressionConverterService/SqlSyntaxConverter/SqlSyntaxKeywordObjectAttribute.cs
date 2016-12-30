using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordObjectAttribute : SqlSyntaxConverterObjectAttribute
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
