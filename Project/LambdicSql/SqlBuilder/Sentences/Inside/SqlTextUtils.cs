using System.Linq;

namespace LambdicSql.SqlBuilder.Sentences.Inside
{
    static class SqlTextUtils
    {
        internal static HSentence Arguments(params Sentence[] args)
            => new HSentence(args) { Separator = ", " };

        internal static Sentence Blanket(params Sentence[] args)
            => Arguments(args).ConcatAround("(", ")");

        internal static HSentence Func(Sentence func, params Sentence[] args)
            => Func(func, ", ", args);

        internal static HSentence FuncSpace(Sentence func, params Sentence[] args)
            => Func(func, " ", args);

        internal static HSentence Clause(Sentence clause, params Sentence[] args)
            => new HSentence(new Sentence[] { clause }.Concat(args)) { IsFunctional = true, Separator = " " };

        internal static HSentence SubClause(Sentence clause, params Sentence[] args)
            => new HSentence(new Sentence[] { clause }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = 1 };

        internal static HSentence Line(params Sentence[] args)
            => new HSentence(args) { EnableChangeLine = false };

        internal static HSentence LineSpace(params Sentence[] args)
             => new HSentence(args) { EnableChangeLine = false, Separator = " " };

        static HSentence Func(Sentence func, string separator, params Sentence[] args)
        {
            var hArgs = new HSentence(args) { Separator = separator }.ConcatToBack(")");
            return new HSentence(Line(func, "("), hArgs) { IsFunctional = true };
        }
    }
}
