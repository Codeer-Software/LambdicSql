namespace LambdicSql.SqlBase.TextParts
{
    abstract class TextWrapper : SqlText
    {
        protected SqlText Core { get; private set; }

        public TextWrapper(SqlText core)
        {
            Core = core;
        }

        public override bool IsSingleLine => Core.IsSingleLine;

        public override bool IsEmpty => Core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlConvertOption option, ParameterInfo paramterInfo) 
            => Core.ToString(isTopLevel, indent, option, paramterInfo);
    }
}
