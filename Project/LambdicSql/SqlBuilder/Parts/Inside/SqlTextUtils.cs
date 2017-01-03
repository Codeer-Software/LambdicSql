using System.Linq;

namespace LambdicSql.SqlBuilder.Parts.Inside
{
    static class SqlTextUtils
    {
        internal static HParts Arguments(params BuildingParts[] args)
            => new HParts(args) { Separator = ", " };

        internal static BuildingParts Blanket(params BuildingParts[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HParts Func(BuildingParts func, params BuildingParts[] args)
            => Func(func, ", ", args);

        internal static HParts FuncSpace(BuildingParts func, params BuildingParts[] args)
            => Func(func, " ", args);

        internal static HParts Clause(BuildingParts clause, params BuildingParts[] args)
            => new HParts(new BuildingParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HParts SubClause(BuildingParts clause, params BuildingParts[] args)
            => new HParts(new BuildingParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HParts Line(params BuildingParts[] args)
            => new HParts(args) { EnableChangeLine = false };

        internal static HParts LineSpace(params BuildingParts[] args)
             => new HParts(args) { EnableChangeLine = false, Separator = " " };

        static HParts Func(BuildingParts func, string separator, params BuildingParts[] args)
        {
            var hArgs = new HParts(args) { Separator = separator }.ConcatToBack(")");
            return new HParts(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
