namespace LambdicSql
{
    public interface IClause
    {
        IClause Clone();
        string ToString(IExpressionDecoder decoder);
    }
}
