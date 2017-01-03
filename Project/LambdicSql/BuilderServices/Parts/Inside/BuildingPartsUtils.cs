using System.Linq;

namespace LambdicSql.BuilderServices.Parts.Inside
{
    static class BuildingPartsUtils
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

        internal static string GetIndent(int indent)
        {
            switch (indent)
            {
                case 0: return string.Empty;
                case 1: return "\t";
                case 2: return "\t\t";
                case 3: return "\t\t\t";
                case 4: return "\t\t\t\t";
                case 5: return "\t\t\t\t\t";
                case 6: return "\t\t\t\t\t\t";
                case 7: return "\t\t\t\t\t\t\t";
                case 8: return "\t\t\t\t\t\t\t\t";
                case 9: return "\t\t\t\t\t\t\t\t\t";
                case 10:return "\t\t\t\t\t\t\t\t\t\t";
            }
            return string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray());
        }
    }
}
