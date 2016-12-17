﻿namespace LambdicSql.SqlBase.TextParts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISqlTextCustomizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        SqlText Custom(SqlText src);
    }

    //TODO やっぱりこいつらはSqlPartsだな
    /// <summary>
    /// Text.
    /// </summary>
    public abstract class SqlText
    {
        /// <summary>
        /// Is single line.
        /// </summary>
        public abstract bool IsSingleLine { get; }

        /// <summary>
        /// Is empty.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="option">Option.</param>
        /// <param name="paramterInfo">ParamterInfo.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(bool isTopLevel, int indent, SqlConvertOption option, ParameterInfo paramterInfo);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract SqlText ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract SqlText ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract SqlText ConcatToBack(string back);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public abstract SqlText Customize(ISqlTextCustomizer customizer);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator SqlText(string text) => new SingleText(text);
    }
}
