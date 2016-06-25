using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class DbInfo
    {
        Dictionary<string, SchemaInfo> _children = new Dictionary<string, SchemaInfo>();
        public IReadOnlyDictionary<string, SchemaInfo> Children => _children;

        public IReadOnlyDictionary<string, ColumnInfo> GetAllColumns()
        {
            var dic = new Dictionary<string, ColumnInfo>();
            foreach (var schema in Children)
            {
                foreach (var table in schema.Value.Children)
                {
                    foreach (var column in table.Value.Children)
                    {
                        dic.Add(column.Value.FullNameText, column.Value);
                    }
                }
            }
            return dic;
        }

        internal SchemaInfo Add(string name)
        {
            SchemaInfo schema;
            if (!_children.TryGetValue(name, out schema))
            {
                schema = new SchemaInfo(name);
                _children.Add(name, schema);
            }
            return schema;
        }
    }
}
