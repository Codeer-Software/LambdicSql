using System;

namespace LambdicSql.Inside
{
    static class InvalitContext
    {
        internal static void Throw(string name) { throw new NotSupportedException("[" + name + "] can't be called outside lambda."); }
        internal static T Throw<T>(string name) { throw new NotSupportedException("[" + name + "] can't be called outside lambda."); }
    }
}
