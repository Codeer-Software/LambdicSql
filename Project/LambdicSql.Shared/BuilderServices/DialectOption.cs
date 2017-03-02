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
    }
}
