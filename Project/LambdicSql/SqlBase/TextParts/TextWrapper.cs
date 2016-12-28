namespace LambdicSql.SqlBase.TextParts
{
    abstract class TextWrapper : ExpressionElement
    {
        protected ExpressionElement Core { get; private set; }

        public TextWrapper(ExpressionElement core)
        {
            Core = core;
        }

        public override bool IsSingleLine(ExpressionConvertingContext context) => Core.IsSingleLine(context);

        public override bool IsEmpty => Core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context) 
            => Core.ToString(isTopLevel, indent, context);
    }
}
