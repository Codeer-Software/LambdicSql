﻿using LambdicSql.BuilderServices.CodeParts;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for object.
    /// </summary>
    public abstract class ObjectConverterAttribute : Attribute
    {
        /// <summary>
        /// Convert object to code.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Parts.</returns>
        public abstract ICode Convert(object obj);
    }
}
