using LambdicSql.ExpressionConverterService;
using System.Collections.Generic;

namespace LambdicSql.SqlBuilder
{
    /// <summary>
    /// Context of SQL conversion.
    /// </summary>
    public class ExpressionConvertingContext
    {
        /// <summary>
        /// Option
        /// </summary>
        public DialectOption Option { get; }

        /// <summary>
        /// Parameter info.
        /// </summary>
        public ParameterInfo ParameterInfo { get; }

        /// <summary>
        /// Object creat info.
        /// </summary>
        public ObjectCreateInfo ObjectCreateInfo { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, bool> WithEntied { get; } = new Dictionary<string, bool>();

        internal ExpressionConvertingContext(DialectOption option)
        {
            Option = option;
            ParameterInfo = new ParameterInfo(option.ParameterPrefix);
        }
    }
}
