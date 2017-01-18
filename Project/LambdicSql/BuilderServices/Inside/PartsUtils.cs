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

        internal static string Join(string separator, string[] value)
        {
            switch (value.Length)
            {
                case 0: return string.Empty;
                case 1: return value[0];
                case 2: return value[0] + separator + value[1];
                case 3: return value[0] + separator + value[1] + separator + value[2];
                case 4: return value[0] + separator + value[1] + separator + value[2] + separator + value[3];
                case 5: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4];
                case 6: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4] + separator + value[5];
                case 7: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4] + separator + value[5] + separator + value[6];
                case 8: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4] + separator + value[5] + separator + value[6] + separator + value[7];
                case 9: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4] + separator + value[5] + separator + value[6] + separator + value[7] + separator + value[8];
                case 10: return value[0] + separator + value[1] + separator + value[2] + separator + value[3] + separator + value[4] + separator + value[5] + separator + value[6] + separator + value[7] + separator + value[8] + separator + value[9];
            }
            return string.Join(separator, value);
        }
    }
}
