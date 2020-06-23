using System.Reflection;

namespace LambdicSql
{
    /// <summary>
    /// This is a method to customize the names of DB tables and columns. The following properties of T will be passed, so change the name appropriately.
    /// </summary>
    /// <param name="prop">Property</param>
    /// <returns>Customized name.If you do not customize, please return null or empty.</returns>
    public delegate string DbDefinitionCustomizerDelegate(PropertyInfo prop);

}
