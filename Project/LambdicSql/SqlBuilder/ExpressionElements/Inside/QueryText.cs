namespace LambdicSql.SqlBuilder.ExpressionElements.Inside
{
    internal class QueryText : TextWrapper
    {
        internal QueryText(ExpressionElement core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context) 
            => base.ToString(false, indent, context);

        public override ExpressionElement ConcatAround(string front, string back) => new QueryText(Core.ConcatAround(front, back));

        public override ExpressionElement ConcatToFront(string front) => new QueryText(Core.ConcatToFront(front));

        public override ExpressionElement ConcatToBack(string back) => new QueryText(Core.ConcatToBack(back));

        public override ExpressionElement Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
