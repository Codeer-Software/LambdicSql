using System.Linq;

namespace LambdicSql.SqlBuilder.Parts.Inside
{
    static class SqlTextUtils
    {
        internal static HBuildingParts Arguments(params BuildingParts[] args)
            => new HBuildingParts(args) { Separator = ", " };

        internal static BuildingParts Blanket(params BuildingParts[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HBuildingParts Func(BuildingParts func, params BuildingParts[] args)
            => Func(func, ", ", args);

        internal static HBuildingParts FuncSpace(BuildingParts func, params BuildingParts[] args)
            => Func(func, " ", args);

        internal static HBuildingParts Clause(BuildingParts clause, params BuildingParts[] args)
            => new HBuildingParts(new BuildingParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HBuildingParts SubClause(BuildingParts clause, params BuildingParts[] args)
            => new HBuildingParts(new BuildingParts[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HBuildingParts Line(params BuildingParts[] args)
            => new HBuildingParts(args) { EnableChangeLine = false };

        internal static HBuildingParts LineSpace(params BuildingParts[] args)
             => new HBuildingParts(args) { EnableChangeLine = false, Separator = " " };

        static HBuildingParts Func(BuildingParts func, string separator, params BuildingParts[] args)
        {
            var hArgs = new HBuildingParts(args) { Separator = separator }.ConcatToBack(")");
            return new HBuildingParts(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
