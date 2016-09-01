namespace LambdicSql.SqlBase
{
    public interface ISqlQuery : ISqlExpressionBase { }

    public interface ISqlQuery<out TSelected> : ISqlExpressionBase<IQuery<TSelected>>, ISqlQuery
    {
        TSelected Body { get; }
    }
}
//TODO T→Entityに変更する
