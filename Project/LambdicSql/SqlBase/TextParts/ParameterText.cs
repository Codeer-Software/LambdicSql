namespace LambdicSql.SqlBase.TextParts
{
    class ParameterText : SingleText
    {
        internal string Text { get; }
        internal ParameterText(string text) : base(text)
        {
            Text = text;
        }

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
