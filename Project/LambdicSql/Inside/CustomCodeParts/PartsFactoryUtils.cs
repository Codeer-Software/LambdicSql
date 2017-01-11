using LambdicSql.BuilderServices.Parts;
using System.Linq;

namespace LambdicSql.Inside.CustomCodeParts
{
    static class PartsFactoryUtils
    {
        internal static HParts Arguments(params CodeParts[] args)
            => new HParts(args) { Separator = ", " };

        internal static CodeParts Blanket(params CodeParts[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HParts Func(CodeParts func, params CodeParts[] args)
            => Func(func, ", ", args);

        internal static HParts FuncSpace(CodeParts func, params CodeParts[] args)
            => Func(func, " ", args);

        internal static HParts Clause(CodeParts clause, params CodeParts[] args)
            => new HParts(new CodeParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HParts SubClause(CodeParts clause, params CodeParts[] args)
            => new HParts(new CodeParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HParts Line(params CodeParts[] args)
            => new HParts(args) { EnableChangeLine = false };

        internal static HParts LineSpace(params CodeParts[] args)
             => new HParts(args) { EnableChangeLine = false, Separator = " " };

        static HParts Func(CodeParts func, string separator, params CodeParts[] args)
        {
            var hArgs = new HParts(args) { Separator = separator }.ConcatToBack(")");
            return new HParts(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
