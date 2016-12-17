namespace LambdicSql.SqlBase.TextParts
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(SqlText core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, SqlConvertOption option, ParameterInfo paramterInfo) 
            => base.ToString(false, indent, option, paramterInfo);

        public override SqlText ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
