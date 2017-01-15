using LambdicSql.BuilderServices.CodeParts;
using System.Linq;

namespace LambdicSql.BuilderServices.Inside
{
    static class PartsFactoryUtils
    {
        internal static HCode Arguments(params Code[] args)
            => new HCode(args) { Separator = ", " };

        internal static Code Blanket(params Code[] args)
            => new AroundCode(Arguments(args), "(", ")");

        internal static HCode Func(Code func, params Code[] args)
            => Func(func, ", ", args);
        
        internal static HCode Clause(Code clause, params Code[] args)
            => new HCode(new Code[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };
        
        internal static HCode Line(params Code[] args)
            => new HCode(args) { EnableChangeLine = false };

        internal static HCode LineSpace(params Code[] args)
             => new HCode(args) { EnableChangeLine = false, Separator = " " };

        static HCode Func(Code func, string separator, params Code[] args)
        {
            var hArgs = new AroundCode(new HCode(args) { Separator = separator },"", ")");
            return new HCode(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
