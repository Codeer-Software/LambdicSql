namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(SqlText core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent)
        {
            if (isTopLevel) return base.ToString(false, indent);
            return Core.ConcatAround("(", ")").ToString(false, indent);
        }

        public override SqlText ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));
    }
}
