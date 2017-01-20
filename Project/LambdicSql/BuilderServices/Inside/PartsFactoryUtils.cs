using LambdicSql.BuilderServices.CodeParts;

namespace LambdicSql.BuilderServices.Inside
{
    static class PartsFactoryUtils
    {
        internal static HCode Arguments(params ICode[] args)
            => new HCode(args) { Separator = ", " };

        internal static ICode Blanket(params ICode[] args)
            => new AroundCode(Arguments(args), "(", ")");

        internal static HCode Func(ICode func, params ICode[] args)
            => Func(func, ", ", args);

        internal static HCode Clause(ICode clause, params ICode[] args)
        {
            var code = new HCode() { AddIndentNewLine = true, Separator = " " };
            code.Add(clause);
            code.AddRange(args);
            return code;
        }

        internal static HCode Line(params ICode[] args)
            => new HCode(args) { EnableChangeLine = false };

        internal static HCode LineSpace(params ICode[] args)
             => new HCode(args) { EnableChangeLine = false, Separator = " " };

        static HCode Func(ICode func, string separator, params ICode[] args)
        {
            var hArgs = new AroundCode(new HCode(args) { Separator = separator },"", ")");
            return new HCode(Line(func, "(".ToCode()), hArgs) { AddIndentNewLine = true };
        }

        internal static SingleTextCode ToCode(this string src)
            => new SingleTextCode(src);
    }
}
