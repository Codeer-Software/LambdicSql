using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.BuilderServices.Inside
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
                case 10: return "\t\t\t\t\t\t\t\t\t\t";
            }

            var array = new char[indent];
            for (int i = 0; i < indent; i++)
            {
                array[i] = '\t';
            }
            return new string(array);
        }

        internal static HCode Line(params ICode[] args)
            => new HCode(args) { EnableChangeLine = false };

        internal static SingleTextCode ToCode(this string src)
            => new SingleTextCode(src);
    }
}
