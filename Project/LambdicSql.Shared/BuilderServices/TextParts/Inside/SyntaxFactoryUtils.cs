using System.Linq;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    //TODO Symbolのリファクタリングが終わった段階で見直し
    static class SyntaxFactoryUtils
    {
        internal static HText Arguments(params TextPartsBase[] args)
            => new HText(args) { Separator = ", " };

        internal static TextPartsBase Blanket(params TextPartsBase[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HText Func(TextPartsBase func, params TextPartsBase[] args)
            => Func(func, ", ", args);

        internal static HText FuncSpace(TextPartsBase func, params TextPartsBase[] args)
            => Func(func, " ", args);

        internal static HText Clause(TextPartsBase clause, params TextPartsBase[] args)
            => new HText(new TextPartsBase[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HText SubClause(TextPartsBase clause, params TextPartsBase[] args)
            => new HText(new TextPartsBase[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HText Line(params TextPartsBase[] args)
            => new HText(args) { EnableChangeLine = false };

        internal static HText LineSpace(params TextPartsBase[] args)
             => new HText(args) { EnableChangeLine = false, Separator = " " };

        static HText Func(TextPartsBase func, string separator, params TextPartsBase[] args)
        {
            var hArgs = new HText(args) { Separator = separator }.ConcatToBack(")");
            return new HText(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
