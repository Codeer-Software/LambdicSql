using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class ColumnInfo
    {
        public Type Type { get; }
        public IReadOnlyList<string> FullName { get; }
        public string FullNameText => string.Join(".", FullName);
        public string Name => FullName[FullName.Count - 1];

        public ColumnInfo(Type type, IReadOnlyList<string> fullName)
        {
            Type = type;
            FullName = fullName;
        }
    }
}
