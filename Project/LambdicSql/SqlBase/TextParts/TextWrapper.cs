namespace LambdicSql.SqlBase.TextParts
{
    /// <summary>
    /// Wrapper.
    /// </summary>
    public abstract class TextWrapper : SqlText
    {
        /// <summary>
        /// 
        /// </summary>
        protected SqlText Core { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core">Core.</param>
        public TextWrapper(SqlText core)
        {
            Core = core;
        }

        /// <summary>
        /// Is single line.
        /// </summary>
        public override bool IsSingleLine => Core.IsSingleLine;

        /// <summary>
        /// Is empty.
        /// </summary>
        public override bool IsEmpty => Core.IsEmpty;

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent) => Core.ToString(isTopLevel, indent);
    }

}
