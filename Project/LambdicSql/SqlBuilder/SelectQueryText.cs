namespace LambdicSql.SqlBase.TextParts
{
    internal class SelectQueryText : TextWrapper
    {
        internal SelectQueryText(ExpressionElement core) : base(core) { }

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
        {
            if (isTopLevel) return base.ToString(false, indent, context);
            return Core.ConcatAround("(", ")").ToString(false, indent, context);
        }

        public override ExpressionElement ConcatAround(string front, string back) => new SelectQueryText(Core.ConcatAround(front, back));

        public override ExpressionElement ConcatToFront(string front) => new SelectQueryText(Core.ConcatToFront(front));

        public override ExpressionElement ConcatToBack(string back) => new SelectQueryText(Core.ConcatToBack(back));

        public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
    }
}
