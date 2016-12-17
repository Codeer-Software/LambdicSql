namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectClauseText : TextWrapper
    {
        internal SelectClauseText(SqlText core) : base(core) { }

        public override SqlText ConcatAround(string front, string back) => new SelectClauseText(Core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new SelectClauseText(Core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new SelectClauseText(Core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
