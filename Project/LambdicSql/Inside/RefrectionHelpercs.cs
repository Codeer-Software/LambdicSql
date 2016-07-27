using System.Reflection;

namespace LambdicSql.Inside
{
    static class RefrectionHelpercs
    {
        internal static string GetPropertyName(this MethodInfo method) => (method.Name.IndexOf("get_") == 0) ? method.Name.Substring(4) : method.Name;
    }
}
