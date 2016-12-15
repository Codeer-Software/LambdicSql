namespace LambdicSql.SqlBase.TextParts
{
    class ParameterText : SingleText
    {
        internal string Text { get; }
        internal ParameterText(string text) : base(text)
        {
            Text = text;
        }
    }
}
