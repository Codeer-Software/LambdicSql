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


        //TODO このあたり、一般にも使えそうなデザインにする

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
        /// Does a Recursive clause exist?
        /// </summary>
        public bool ExistRecursiveClause { get; set; }

        /// <summary>
        /// Does a Recursive clause exist?
        /// </summary>
        public bool IsRowsParameterDirectValue { get; set; }

        /// <summary>
        /// Separator between Current and Date.
        /// It affects CurrentDate, CurrentTime, CurrentTimeStamp.
        /// </summary>
        public string CurrentDateTimeSeparator { get; set; } = "_";
    }
}
