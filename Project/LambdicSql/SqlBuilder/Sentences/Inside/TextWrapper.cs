namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    abstract class TextWrapper : Sentence
    {
        protected Sentence Core { get; private set; }

        public TextWrapper(Sentence core)
        {
            Core = core;
        }

        public override bool IsSingleLine(SqlBuildingContext context) => Core.IsSingleLine(context);

        public override bool IsEmpty => Core.IsEmpty;

        public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context) 
            => Core.ToString(isTopLevel, indent, context);
    }
}
