using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Change object to paramter in SQL.
    /// </summary>
    public class CustomizeObjectToParameter : ICodeCustomizer
    {
        /// <summary>
        /// Visit and customize.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <returns>Destination.</returns>
        public ICode Visit(ICode src)
        {
            var table = src as DbTableCode;
            if (table != null)
            {
                return new ParameterCode(table.Info.SqlFullName);
            }
            var col = src as DbColumnCode;
            if (col != null)
            {
                return new ParameterCode(col.ColumnName);
            }
            return src;

        }
    }
}
