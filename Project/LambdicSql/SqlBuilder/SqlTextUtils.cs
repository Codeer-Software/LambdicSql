using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    static class SqlTextUtils
    {
        internal static HText Arguments(params ExpressionElement[] args)
            => new HText(args) { Separator = ", " };

        internal static ExpressionElement Blanket(params ExpressionElement[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HText Func(ExpressionElement func, params ExpressionElement[] args)
            => Func(func, ", ", args);

        internal static HText FuncSpace(ExpressionElement func, params ExpressionElement[] args)
            => Func(func, " ", args);

        internal static HText Clause(ExpressionElement clause, params ExpressionElement[] args)
            => new HText(new ExpressionElement[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HText SubClause(ExpressionElement clause, params ExpressionElement[] args)
            => new HText(new ExpressionElement[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HText Line(params ExpressionElement[] args)
            => new HText(args) { EnableChangeLine = false };

        internal static HText LineSpace(params ExpressionElement[] args)
             => new HText(args) { EnableChangeLine = false, Separator = " " };

        static HText Func(ExpressionElement func, string separator, params ExpressionElement[] args)
        {
            var hArgs = new HText(args) { Separator = separator }.ConcatToBack(")");
            return new HText(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
