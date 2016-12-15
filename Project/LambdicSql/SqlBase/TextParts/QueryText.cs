namespace LambdicSql.SqlBase.TextParts
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(SqlText core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent) => base.ToString(false, indent);

        public override SqlText ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));
    }
}
