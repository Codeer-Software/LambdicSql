namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectClauseText : TextWrapper
    {
        internal SelectClauseText(SqlText core) : base(core) { }

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatAround(string front, string back) => new SelectClauseText(Core.ConcatAround(front, back));

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public override SqlText ConcatToFront(string front) => new SelectClauseText(Core.ConcatToFront(front));

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public override SqlText ConcatToBack(string back) => new SelectClauseText(Core.ConcatToBack(back));
    }
}
