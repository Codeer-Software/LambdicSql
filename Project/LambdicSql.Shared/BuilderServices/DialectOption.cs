namespace LambdicSql.BuilderServices
{
    /// <summary>
    /// Option of to convert expression to sql text.
    /// </summary>
    public class DialectOption
    {
        string _stringAddOperator = " + ";

        /// <summary>
        /// Connection's type fullName.
        /// </summary>
        public string ConnectionTypeFullName { get; set; }

        /// <summary>
        /// String addtional operator.
        /// Default is +.
        /// </summary>
        public string StringAddOperator
        {
            get
            {
                return _stringAddOperator;
            }
            set
            {
                _stringAddOperator = " " + value.Trim() + " ";
            }
        }

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
        /// Separator between Current and Date.
        /// It affects CurrentDate, CurrentTime, CurrentTimeStamp.
        /// </summary>
        public string CurrentDateTimeSeparator { get; set; } = "_";
    }
}
