namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(Sentence core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
        {
            if (isTopLevel) return base.ToString(false, indent, context);
            return Core.ConcatAround("(", ")").ToString(false, indent, context);
        }

        public override Sentence ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        public override Sentence ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        public override Sentence ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));

        public override Sentence Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
