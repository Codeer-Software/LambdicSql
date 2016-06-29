using System;

namespace LambdicSql.Inside
{
    class TypeAndText
    {
        internal Type Type { get; }
        internal string Text { get; }
        internal TypeAndText(Type type, string text)
        {
            Type = type;
            Text = text;
        }
        public override string ToString() => Text;
    }
}
