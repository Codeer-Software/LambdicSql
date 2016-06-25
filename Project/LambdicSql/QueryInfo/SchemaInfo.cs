using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SchemaInfo
    {
        Dictionary<string, TableInfo> _children = new Dictionary<string, TableInfo>();

        public string Name { get; }
        public IReadOnlyDictionary<string, TableInfo> Children => _children;

        public SchemaInfo(string name)
        {
            Name = name;
        }

        internal TableInfo Add(string[] fullName)
        {
            var name = fullName[fullName.Length - 1];
            TableInfo table;
            if (!_children.TryGetValue(name, out table))
            {
                table = new TableInfo(fullName);
                _children.Add(name, table);
            }
            return table;
        }
    }
}
