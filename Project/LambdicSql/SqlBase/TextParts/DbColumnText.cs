﻿using System;
using System.Linq;

namespace LambdicSql.SqlBase.TextParts
{
    class DbColumnText : SqlText
    {
        string _front = string.Empty;
        string _back = string.Empty;
        bool _columnOnly;

        internal ColumnInfo Info { get; private set; }

        string ColumnName => _columnOnly ? Info.SqlColumnName : Info.SqlFullName;

        internal SqlText ToColumnOnly() =>
            new DbColumnText(Info, true, _front, _back);

        internal DbColumnText(ColumnInfo info)
        {
            Info = info;
        }

        internal DbColumnText(ColumnInfo info, bool columnOnly)
        {
            Info = info;
            _columnOnly = columnOnly;
        }

        DbColumnText(ColumnInfo info, bool columnOnly, string front, string back)
        {
            Info = info;
            _front = front;
            _back = back;
            _columnOnly = columnOnly;
        }

        public override bool IsSingleLine(SqlConvertingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + ColumnName + _back;

        public override SqlText ConcatAround(string front, string back) 
            => new DbColumnText(Info, _columnOnly, front + _front, _back + back);

        public override SqlText ConcatToFront(string front) 
            => new DbColumnText(Info, _columnOnly, front + _front, _back);

        public override SqlText ConcatToBack(string back) 
            => new DbColumnText(Info, _columnOnly, _front, _back + back);

        public override SqlText Customize(ISqlTextCustomizer customizer) 
            => customizer.Custom(this);
    }
}
