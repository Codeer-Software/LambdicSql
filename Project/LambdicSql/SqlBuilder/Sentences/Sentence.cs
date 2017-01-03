namespace LambdicSql.SqlBuilder.Sentences
{
    //TODO \tをテーブル化すると早くなる
    //抽象メソッドをなくしても早くなる
    //string.joinもEX
    //Linqをなくしても早くなる

    //SqlPartsかな
    //でネームスペースはSqlBuilder

    //どの辺まで公開するか決めないと

    //それから大幅にフォルダ構成を変える
    //Baseとかは良くない
    //Insideもね

    /// <summary>
    /// Text.
    /// </summary>
    public abstract class Sentence
    {
        /// <summary>
        /// Is single line.
        /// </summary>
        public abstract bool IsSingleLine(SqlBuildingContext context);

        /// <summary>
        /// Is empty.
        /// </summary>
        public abstract bool IsEmpty { get; }
        
        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="isTopLevel">Is top level.</param>
        /// <param name="indent">Indent.</param>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public abstract string ToString(bool isTopLevel, int indent, SqlBuildingContext context);

        /// <summary>
        /// Concat to front and back.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <param name="back">Back.</param>
        /// <returns>Text.</returns>
        public abstract Sentence ConcatAround(string front, string back);

        /// <summary>
        /// Concat to front.
        /// </summary>
        /// <param name="front">Front.</param>
        /// <returns>Text.</returns>
        public abstract Sentence ConcatToFront(string front);

        /// <summary>
        /// Concat to back.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public abstract Sentence ConcatToBack(string back);

        /// <summary>
        /// Customize.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Customized SqlText.</returns>
        public abstract Sentence Customize(ISqlTextCustomizer customizer);

        /// <summary>
        /// Convert string to IText.
        /// </summary>
        /// <param name="text">string.</param>
        public static implicit operator Sentence(string text) => new SingleTextSentence(text);
    }
}
