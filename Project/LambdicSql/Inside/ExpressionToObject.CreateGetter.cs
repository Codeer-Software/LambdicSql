using System;
using System.Reflection;

namespace LambdicSql.Inside
{
    static partial class ExpressionToObject
    {
        static IGetter CreateGetter(Type[] args)
        {
            switch (args.Length)
            {
                case 0: return Activator.CreateInstance(typeof(GetterCore), true) as IGetter;
                case 1: return Activator.CreateInstance(typeof(GetterCore<>).MakeGenericType(args[0]), true) as IGetter;
            }
            throw new NotSupportedException();
        }
    }
}
