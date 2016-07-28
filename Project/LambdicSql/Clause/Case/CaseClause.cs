using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Case
{
    public class CaseClause : IClause
    {
        //TODO　LikeはWordsかなー　でもLikeとInか  Between InにはSubクエリが使えるしなー x.Inとかかけたらいいけど。 bool を返したらまあいいか。

        //★左は値でなければならないかな?→いやDBのバリューでもいいんじゃない？
        //★両方、バリューでもなんでも良い
        //Case(db=>x).WhenThen(value, value)

        string _text;
        public CaseClause(string text) { _text = text; }
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => _text;
    }
}
