using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.MultiplatformCompatibe;
using System;
using System.Collections.Generic;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumToStringConverterAttribute : ObjectConverterAttribute
    {
        Dictionary<string, ICode> _resolver;

        /// <summary>
        /// Convert object to code.
        /// </summary>
        /// <param name="value">Object.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(object value)
        {
            lock (this)
            {
                if (_resolver == null)
                {
                    _resolver = new Dictionary<string, ICode>();
                    var type = value.GetType();
                    foreach (var e in type.GetFieldsEx())
                    {
                        var fieldName = e.GetAttribute<FieldSqlNameAttribute>();
                        var sqlName = fieldName == null ? e.Name.ToUpper() : fieldName.Name;
                        _resolver.Add(e.Name, sqlName.ToCode());
                    }
                }
            }
            return _resolver[value.ToString()];
        }
    }
}
