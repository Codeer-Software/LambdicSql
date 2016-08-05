using System;

namespace LambdicSql.Inside
{
    static partial class ExpressionToObject
    {
        static IGetter CreateGetter(Type[] args)
        {
            switch (args.Length)
            {
                case 0: return Activator.CreateInstance(typeof(GetterCore), true) as IGetter;
                case 1: return Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(args), true) as IGetter;
                case 2: return Activator.CreateInstance(typeof(GetterCore<,>).MakeGenericType(args), true) as IGetter;
                case 3: return Activator.CreateInstance(typeof(GetterCore<,,>).MakeGenericType(args), true) as IGetter;
                case 4: return Activator.CreateInstance(typeof(GetterCore<,,,>).MakeGenericType(args), true) as IGetter;
                case 5: return Activator.CreateInstance(typeof(GetterCore<,,,,>).MakeGenericType(args), true) as IGetter;
                case 6: return Activator.CreateInstance(typeof(GetterCore<,,,,,>).MakeGenericType(args), true) as IGetter;
                case 7: return Activator.CreateInstance(typeof(GetterCore<,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 8: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 9: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 10: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 11: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 12: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 13: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 14: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 15: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 16: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 17: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 18: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 19: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 20: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 21: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 22: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 23: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 24: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 25: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 26: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 27: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 28: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
                case 29: return Activator.CreateInstance(typeof(GetterCore<,,,,,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(args), true) as IGetter;
            }
            throw new NotSupportedException();
        }
    }
}
