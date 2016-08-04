using System;

namespace LambdicSql.Inside
{
    class DecodedInfo
    {
        internal Type Type { get; }
        internal string Text { get; }
        internal DecodedInfo(Type type, string text)
        {
            Type = type;
            Text = text;
        }
        public override string ToString() => Text;
    }
}
