namespace LambdicSql.SqlBase.TextParts
{
    abstract class TextWrapper : SqlText
    {
        protected SqlText Core { get; private set; }

        public TextWrapper(SqlText core)
        {
            Core = core;
        }

        public override bool IsSingleLine(SqlConvertingContext context) => Core.IsSingleLine(context);

        public override bool IsEmpty => Core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context) 
            => Core.ToString(isTopLevel, indent, context);
    }
}
