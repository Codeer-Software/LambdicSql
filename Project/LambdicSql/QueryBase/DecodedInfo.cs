using System;

namespace LambdicSql.QueryBase
{
    public class DecodedInfo
    {
        public Type Type { get; }
        public string Text { get; }

        internal DecodedInfo(Type type, string text)
        {
            Type = type;
            Text = text;
        }

        public override string ToString() => Text;
    }
}
