namespace LambdicSql.SqlBase.TextParts
{
    static class SqlTextUtils
    {
        public static SqlText Func(string func, params SqlText[] args)
            => Func(func, ", ", args);

        public static SqlText FuncSpace(string func, params SqlText[] args)
            => Func(func, " ", args);

        static SqlText Func(string func, string separator, params SqlText[] args)
        {
            var hArgs = new HText(args) { Separator = separator }.ConcatToBack(")");
            return new HText(func + "(", hArgs) { IsFunctional = true };
        }
    }
}
