namespace LambdicSql.SqlBase.TextParts
{
    internal class ParameterText : SingleText
    {
        public string Text { get; }
        public ParameterText(string text) : base(text)
        {
            Text = text;
        }
    }
}
