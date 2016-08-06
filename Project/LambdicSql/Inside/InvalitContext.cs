using System;

namespace LambdicSql.Inside
{
    static class InvalitContext
    {
        internal static T Throw<T>(string name) { throw new NotSupportedException("[" + name + "] can't call out of lambda."); }
    }
}
