namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(SqlText core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            if (isTopLevel) return base.ToString(false, indent, context);
            return Core.ConcatAround("(", ")").ToString(false, indent, context);
        }

        public override SqlText ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
