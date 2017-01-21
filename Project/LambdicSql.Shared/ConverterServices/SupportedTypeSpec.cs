using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.ConverterServices.Inside
{
    static class SupportedTypeSpec
    {
        static List<Type> _supported = new List<Type>();

        static SupportedTypeSpec()
        {
            _supported.Add(typeof(object));
            _supported.Add(typeof(string));
            _supported.Add(typeof(bool));
            _supported.Add(typeof(bool?));
            _supported.Add(typeof(byte));
            _supported.Add(typeof(byte?));
            _supported.Add(typeof(short));
            _supported.Add(typeof(short?));
            _supported.Add(typeof(int));
            _supported.Add(typeof(int?));
            _supported.Add(typeof(long));
            _supported.Add(typeof(long?));
            _supported.Add(typeof(float));
            _supported.Add(typeof(float?));
            _supported.Add(typeof(double));
            _supported.Add(typeof(double?));
            _supported.Add(typeof(decimal));
            _supported.Add(typeof(decimal?));
            _supported.Add(typeof(DateTime));
            _supported.Add(typeof(DateTime?));
            _supported.Add(typeof(DateTimeOffset));
            _supported.Add(typeof(DateTimeOffset?));
            _supported.Add(typeof(TimeSpan));
            _supported.Add(typeof(TimeSpan?));
            _supported.Add(typeof(byte[]));
            _supported.Add(typeof(char[]));
        }

        public static bool IsSupported(Type type)
        {
            lock (_supported)
            {
                return _supported.Contains(type);
            }
        }

        internal static object ConvertArray(Type arrayType, IEnumerable<object> src)
        {
            if (arrayType == typeof(byte[])) return src.Cast<byte>().ToArray();
            else if (arrayType == typeof(char[])) return src.Cast<char>().ToArray();
            throw new NotSupportedException();
        }
    }
}
