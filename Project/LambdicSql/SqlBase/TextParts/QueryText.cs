namespace LambdicSql.SqlBase.TextParts
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(SqlText core) : base(core) { }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <returns>Text.</returns>
        public override string ToString(bool isTopLevel, int indent) => base.ToString(false, indent);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override SqlText ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));
    }
}
