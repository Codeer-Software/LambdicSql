using System.Linq;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    //TODO Symbolのリファクタリングが終わった段階で見直し
    static class SyntaxFactoryUtils
    {
        internal static HSyntax Arguments(params Syntax[] args)
            => new HSyntax(args) { Separator = ", " };

        internal static Syntax Blanket(params Syntax[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HSyntax Func(Syntax func, params Syntax[] args)
            => Func(func, ", ", args);

        internal static HSyntax FuncSpace(Syntax func, params Syntax[] args)
            => Func(func, " ", args);

        internal static HSyntax Clause(Syntax clause, params Syntax[] args)
            => new HSyntax(new Syntax[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HSyntax SubClause(Syntax clause, params Syntax[] args)
            => new HSyntax(new Syntax[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HSyntax Line(params Syntax[] args)
            => new HSyntax(args) { EnableChangeLine = false };

        internal static HSyntax LineSpace(params Syntax[] args)
             => new HSyntax(args) { EnableChangeLine = false, Separator = " " };

        static HSyntax Func(Syntax func, string separator, params Syntax[] args)
        {
            var hArgs = new HSyntax(args) { Separator = separator }.ConcatToBack(")");
            return new HSyntax(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
