﻿using LambdicSql.BuilderServices.Code;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class KeywordObjectConverterAttribute : SymbolConverterObjectAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the ToString of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert object to code parts.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Parts.</returns>
        public override Parts Convert(object obj)
            => obj == null ? string.Empty :
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;
    }
}