using LambdicSql.SqlBuilder.Sentences;

namespace LambdicSql.ConverterService.SqlSyntaxes
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
        public override Sentence Convert(object obj)
            => obj == null ? string.Empty :
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;
    }
}
