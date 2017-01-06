using System.Linq;

namespace LambdicSql.BuilderServices.Code.Inside
{
    //TODO Symbolのリファクタリングが終わった段階で見直し
    static class PartsFactoryUtils
    {
        internal static HParts Arguments(params Parts[] args)
            => new HParts(args) { Separator = ", " };

        internal static Parts Blanket(params Parts[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HParts Func(Parts func, params Parts[] args)
            => Func(func, ", ", args);

        internal static HParts FuncSpace(Parts func, params Parts[] args)
            => Func(func, " ", args);

        internal static HParts Clause(Parts clause, params Parts[] args)
            => new HParts(new Parts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HParts SubClause(Parts clause, params Parts[] args)
            => new HParts(new Parts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HParts Line(params Parts[] args)
            => new HParts(args) { EnableChangeLine = false };

        internal static HParts LineSpace(params Parts[] args)
             => new HParts(args) { EnableChangeLine = false, Separator = " " };

        static HParts Func(Parts func, string separator, params Parts[] args)
        {
            var hArgs = new HParts(args) { Separator = separator }.ConcatToBack(")");
            return new HParts(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
