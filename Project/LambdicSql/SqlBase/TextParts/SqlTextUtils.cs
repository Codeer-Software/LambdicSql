using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    static class SqlTextUtils
    {
        internal static HText Arguments(params SqlText[] args)
            => new HText(args) { Separator = ", " };

        internal static SqlText Blanket(params SqlText[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HText Func(SqlText func, params SqlText[] args)
            => Func(func, ", ", args);

        internal static HText FuncSpace(SqlText func, params SqlText[] args)
            => Func(func, " ", args);

        internal static HText Clause(SqlText clause, params SqlText[] args)
            => new HText(new SqlText[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HText SubClause(SqlText clause, params SqlText[] args)
            => new HText(new SqlText[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HText Line(params SqlText[] args)
            => new HText(args) { EnableChangeLine = false };

        internal static HText LineSpace(params SqlText[] args)
             => new HText(args) { EnableChangeLine = false, Separator = " " };

        static HText Func(SqlText func, string separator, params SqlText[] args)
        {
            var hArgs = new HText(args) { Separator = separator }.ConcatToBack(")");
            return new HText(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
