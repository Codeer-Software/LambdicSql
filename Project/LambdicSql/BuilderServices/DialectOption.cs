namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Option of to convert expression to sql text.
    /// </summary>
    public class DialectOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionTypeFullName { get; set; }

        /// <summary>
        /// String addtional operator.
        /// Default is +.
        /// </summary>
        public string StringAddOperator { get; set; } = "+";

        /// <summary>
        /// Parameter prefix.
        /// Defualt is @.
        /// </summary>
        public string ParameterPrefix { get; set; } = "@";

        /// <summary>
        /// 
        /// </summary>
        public bool ExistRecursive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentDateTimeSeparator { get; set; } = "_";
    }
}
