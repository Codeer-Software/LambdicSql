namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(Sentence core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context) 
            => base.ToString(false, indent, context);

        public override Sentence ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        public override Sentence ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        public override Sentence ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));

        public override Sentence Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
