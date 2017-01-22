using LambdicSql.MultiplatformCompatibe;
using System;

namespace LambdicSql.ConverterServices.Inside
{
    internal static partial class ExpressionToObject
    {
        static IGetter CreateGetter(Type[] args)
        {
            switch (args.Length)
            {
                case 0: return ReflectionAdapter.CreateInstance(typeof(GetterCore), true) as IGetter;
                case 1: return ReflectionAdapter.CreateInstance(typeof(GetterCore<>).MakeGenericType(args), true) as IGetter;
                case 2: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,>).MakeGenericType(args), true) as IGetter;
                case 3: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,>).MakeGenericType(args), true) as IGetter;
                case 4: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,>).MakeGenericType(args), true) as IGetter;
                case 5: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,>).MakeGenericType(args), true) as IGetter;
                case 6: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,>).MakeGenericType(args), true) as IGetter;
                case 7: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 8: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 9: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 10: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 11: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 12: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 13: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 14: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 15: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 16: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 17: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 18: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 19: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 20: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 21: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 22: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 23: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 24: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 25: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 26: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 27: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 28: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 29: return ReflectionAdapter.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
            }
            throw new NotSupportedException("The maximum number of methods arguments that can be used is 30.");
        }
    }
}
