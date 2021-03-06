﻿using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for member. You can define system tables.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MemberTableConverterAttribute : Attribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemberTableConverterAttribute() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name.</param>
        public MemberTableConverterAttribute(string name) => Name = name;
    }
}
