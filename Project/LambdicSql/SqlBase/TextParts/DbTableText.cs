using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    class DbTableText : SqlText
    {
        TableInfo _info;
        string _front = string.Empty;
        string _back = string.Empty;

        internal DbTableText(TableInfo info)
        {
            _info = info;
        }

        DbTableText(TableInfo info, string front, string back)
        {
            _info = info;
            _front = front;
            _back = back;
        }

        public override bool IsSingleLine => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + _info.SqlFullName + _back;

        public override SqlText ConcatAround(string front, string back) 
            => new DbTableText(_info, front + _front, _back + back);

        public override SqlText ConcatToFront(string front) 
            => new DbTableText(_info, front + _front, _back);

        public override SqlText ConcatToBack(string back) 
            => new DbTableText(_info, _front, _back + back);

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
