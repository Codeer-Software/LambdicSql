using System;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Even if IMethodChain is not taken as the first argument, it will be recognized as a method chain.
    /// </summary>
    public class ForcedMethodGroupAttribute : Attribute { }
}
