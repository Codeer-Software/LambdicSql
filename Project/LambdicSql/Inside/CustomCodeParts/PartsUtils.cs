using System.Linq;

namespace LambdicSql.Inside.CustomCodeParts
{
    static class PartsUtils
    {
        internal static string GetIndent(int indent)
        {
            switch (indent)
            {
                case 0: return string.Empty;
                case 1: return "\t";
                case 2: return "\t\t";
                case 3: return "\t\t\t";
                case 4: return "\t\t\t\t";
                case 5: return "\t\t\t\t\t";
                case 6: return "\t\t\t\t\t\t";
                case 7: return "\t\t\t\t\t\t\t";
                case 8: return "\t\t\t\t\t\t\t\t";
                case 9: return "\t\t\t\t\t\t\t\t\t";
                case 10:return "\t\t\t\t\t\t\t\t\t\t";
            }
            return string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray());
        }
    }
}
