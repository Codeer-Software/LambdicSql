﻿using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// SQL Symbols.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// Use[using static LambdicSql.Keywords;], you can use to write natural SQL.
    /// </summary>
    public static partial class Symbols
    {
        /// <summary>
        /// BFILE
        /// </summary>
        /// <returns>BFILE</returns>
        [KeywordMethodConverter]
        public static IDataType BFile() => InvalitContext.Throw<IDataType>(nameof(BFile));

        /// <summary>
        /// BIGINT
        /// </summary>
        /// <returns>BIGINT</returns>
        [KeywordMethodConverter]
        public static IDataType BigInt() => InvalitContext.Throw<IDataType>(nameof(BigInt));

        /// <summary>
        /// BIGSERIAL
        /// </summary>
        /// <returns>BIGSERIAL</returns>
        [KeywordMethodConverter]
        public static IDataType BigSerial() => InvalitContext.Throw<IDataType>(nameof(BigSerial));

        /// <summary>
        /// BINARY
        /// </summary>
        /// <returns>BINARY</returns>
        [KeywordMethodConverter]
        public static IDataType Binary() => InvalitContext.Throw<IDataType>(nameof(Binary));

        /// <summary>
        /// BINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BINARY</returns>
        [FuncConverter]
        public static IDataType Binary(int n) => InvalitContext.Throw<IDataType>(nameof(Binary));

        /// <summary>
        /// BINARY_DOUBLE
        /// </summary>
        /// <returns>BINARY_DOUBLE</returns>
        [KeywordMethodConverter]
        public static IDataType Binary_Double() => InvalitContext.Throw<IDataType>(nameof(Binary_Double));

        /// <summary>
        /// BINARY_FLOAT
        /// </summary>
        /// <returns>BINARY_FLOAT</returns>
        [KeywordMethodConverter]
        public static IDataType Binary_Float() => InvalitContext.Throw<IDataType>(nameof(Binary_Float));

        /// <summary>
        /// BIT
        /// </summary>
        /// <returns>BIT</returns>
        [KeywordMethodConverter]
        public static IDataType Bit() => InvalitContext.Throw<IDataType>(nameof(Bit));

        /// <summary>
        /// BIT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT</returns>
        [FuncConverter]
        public static IDataType Bit(int n) => InvalitContext.Throw<IDataType>(nameof(Bit));

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <returns>BIT VARYING</returns>
        [KeywordMethodConverter(Name = "BIT VARYING")]
        public static IDataType BitVarying() => InvalitContext.Throw<IDataType>(nameof(BitVarying));

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT VARYING</returns>
        [FuncConverter(Name = "BIT VARYING")]
        public static IDataType BitVarying(int n) => InvalitContext.Throw<IDataType>(nameof(BitVarying));

        /// <summary>
        /// BLOB
        /// </summary>
        /// <returns>BLOB</returns>
        [KeywordMethodConverter]
        public static IDataType Blob() => InvalitContext.Throw<IDataType>(nameof(Blob));

        /// <summary>
        /// BOOLEAN
        /// </summary>
        /// <returns>BOOLEAN</returns>
        [KeywordMethodConverter]
        public static IDataType Boolean() => InvalitContext.Throw<IDataType>(nameof(Boolean));

        /// <summary>
        /// BOX
        /// </summary>
        /// <returns>BOX</returns>
        [KeywordMethodConverter]
        public static IDataType Box() => InvalitContext.Throw<IDataType>(nameof(Box));

        /// <summary>
        /// BYTEA
        /// </summary>
        /// <returns>BYTEA</returns>
        [KeywordMethodConverter]
        public static IDataType Bytea() => InvalitContext.Throw<IDataType>(nameof(Bytea));

        //TODO CHAR(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// CHAR
        /// </summary>
        /// <returns>CHAR</returns>
        [KeywordMethodConverter]
        public static IDataType Char() => InvalitContext.Throw<IDataType>(nameof(Char));

        /// <summary>
        /// CHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHAR</returns>
        [FuncConverter]
        public static IDataType Char(int n) => InvalitContext.Throw<IDataType>(nameof(Char));

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <returns>CHARACTER</returns>
        [KeywordMethodConverter]
        public static IDataType Character() => InvalitContext.Throw<IDataType>(nameof(Character));

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER</returns>
        [FuncConverter]
        public static IDataType Character(int n) => InvalitContext.Throw<IDataType>(nameof(Character));

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <returns>CHARACTER VARYING</returns>
        [KeywordMethodConverter(Name = "CHARACTER VARYING")]
        public static IDataType CharacterVarying() => InvalitContext.Throw<IDataType>(nameof(CharacterVarying));

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER VARYING</returns>
        [FuncConverter(Name = "CHARACTER VARYING")]
        public static IDataType CharacterVarying(int n) => InvalitContext.Throw<IDataType>(nameof(CharacterVarying));

        /// <summary>
        /// CIDR
        /// </summary>
        /// <returns>CIDR</returns>
        [KeywordMethodConverter]
        public static IDataType Cidr() => InvalitContext.Throw<IDataType>(nameof(Cidr));

        /// <summary>
        /// CIRCLE
        /// </summary>
        /// <returns>CIRCLE</returns>
        [KeywordMethodConverter]
        public static IDataType Circle() => InvalitContext.Throw<IDataType>(nameof(Circle));

        /// <summary>
        /// CLOB
        /// </summary>
        /// <returns>CLOB</returns>
        [KeywordMethodConverter]
        public static IDataType Clob() => InvalitContext.Throw<IDataType>(nameof(Clob));

        /// <summary>
        /// CURRENCY
        /// </summary>
        /// <returns>CURRENCY</returns>
        [KeywordMethodConverter]
        public static IDataType Currency() => InvalitContext.Throw<IDataType>(nameof(Currency));

        /// <summary>
        /// DATE
        /// </summary>
        /// <returns>DATE</returns>
        [KeywordMethodConverter]
        public static IDataType Date() => InvalitContext.Throw<IDataType>(nameof(Date));

        /// <summary>
        /// DATETIME
        /// </summary>
        /// <returns>DATETIME</returns>
        [KeywordMethodConverter]
        public static IDataType DateTime() => InvalitContext.Throw<IDataType>(nameof(DateTime));

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <returns>DATETIME2</returns>
        [KeywordMethodConverter]
        public static IDataType DateTime2() => InvalitContext.Throw<IDataType>(nameof(DateTime2));

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIME2</returns>
        [FuncConverter]
        public static IDataType DateTime2(int n) => InvalitContext.Throw<IDataType>(nameof(DateTime2));

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <returns>DATETIMEOFFSET</returns>
        [KeywordMethodConverter]
        public static IDataType DateTimeOffset() => InvalitContext.Throw<IDataType>(nameof(DateTimeOffset));

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIMEOFFSET</returns>
        [FuncConverter]
        public static IDataType DateTimeOffset(int n) => InvalitContext.Throw<IDataType>(nameof(DateTimeOffset));

        /// <summary>
        /// DBCLOB
        /// </summary>
        /// <returns>DBCLOB</returns>
        [KeywordMethodConverter]
        public static IDataType DbClob() => InvalitContext.Throw<IDataType>(nameof(DbClob));

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <returns>DECIMAL</returns>
        [KeywordMethodConverter]
        public static IDataType Decimal() => InvalitContext.Throw<IDataType>(nameof(Decimal));

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>DECIMAL</returns>
        [KeywordMethodConverter]
        public static IDataType Decimal(int precision) => InvalitContext.Throw<IDataType>(nameof(Decimal));


        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>DECIMAL</returns>
        [KeywordMethodConverter]
        public static IDataType Decimal(int precision, int scale) => InvalitContext.Throw<IDataType>(nameof(Decimal));

        /// <summary>
        /// DOUBLE
        /// </summary>
        /// <returns>DOUBLE</returns>
        [KeywordMethodConverter]
        public static IDataType Double() => InvalitContext.Throw<IDataType>(nameof(Double));

        /// <summary>
        /// DOUBLE PRECISION
        /// </summary>
        /// <returns>DOUBLE PRECISION</returns>
        [KeywordMethodConverter(Name = "DOUBLE PRECISION")]
        public static IDataType DoublePrecision() => InvalitContext.Throw<IDataType>(nameof(DoublePrecision));

        /// <summary>
        /// ENUM
        /// </summary>
        /// <returns>ENUM</returns>
        [KeywordMethodConverter]
        public static IDataType Enum() => InvalitContext.Throw<IDataType>(nameof(Enum));

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <returns>FLOAT</returns>
        [KeywordMethodConverter]
        public static IDataType Float() => InvalitContext.Throw<IDataType>(nameof(Float));

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>FLOAT</returns>
        [FuncConverter]
        public static IDataType Float(int n) => InvalitContext.Throw<IDataType>(nameof(Float));

        /// <summary>
        /// GRAPHIC
        /// </summary>
        /// <returns>GRAPHIC</returns>
        [KeywordMethodConverter]
        public static IDataType Graphic() => InvalitContext.Throw<IDataType>(nameof(Graphic));

        /// <summary>
        /// IMAGE
        /// </summary>
        /// <returns>IMAGE</returns>
        [KeywordMethodConverter]
        public static IDataType Image() => InvalitContext.Throw<IDataType>(nameof(Image));

        /// <summary>
        /// INET
        /// </summary>
        /// <returns>INET</returns>
        [KeywordMethodConverter]
        public static IDataType Inet() => InvalitContext.Throw<IDataType>(nameof(Inet));

        /// <summary>
        /// INT
        /// </summary>
        /// <returns>INT</returns>
        [KeywordMethodConverter]
        public static IDataType Int() => InvalitContext.Throw<IDataType>(nameof(Int));

        /// <summary>
        /// INT2
        /// </summary>
        /// <returns>INT2</returns>
        [KeywordMethodConverter]
        public static IDataType Int2() => InvalitContext.Throw<IDataType>(nameof(Int2));

        /// <summary>
        /// INT8
        /// </summary>
        /// <returns>INT8</returns>
        [KeywordMethodConverter]
        public static IDataType Int8() => InvalitContext.Throw<IDataType>(nameof(Int8));

        /// <summary>
        /// INTEGER
        /// </summary>
        /// <returns>INTEGER</returns>
        [KeywordMethodConverter]
        public static IDataType Integer() => InvalitContext.Throw<IDataType>(nameof(Integer));

        //TODO interval [ fields ] [ (p) ] fieldはenumにするか
        //TODO オラクル考慮
        /// <summary>
        /// INTERVAL
        /// </summary>
        /// <returns>INTERVAL</returns>
        [KeywordMethodConverter]
        public static IDataType Interval() => InvalitContext.Throw<IDataType>(nameof(Interval));

        /// <summary>
        /// JSON
        /// </summary>
        /// <returns>JSON</returns>
        [KeywordMethodConverter]
        public static IDataType Json() => InvalitContext.Throw<IDataType>(nameof(Json));

        /// <summary>
        /// JSONB
        /// </summary>
        /// <returns>JSONB</returns>
        [KeywordMethodConverter]
        public static IDataType JsonB() => InvalitContext.Throw<IDataType>(nameof(JsonB));

        /// <summary>
        /// LINE
        /// </summary>
        /// <returns>LINE</returns>
        [KeywordMethodConverter]
        public static IDataType Line() => InvalitContext.Throw<IDataType>(nameof(Line));

        /// <summary>
        /// LONG
        /// </summary>
        /// <returns>LONG</returns>
        [KeywordMethodConverter]
        public static IDataType Long() => InvalitContext.Throw<IDataType>(nameof(Long));

        /// <summary>
        /// LONG RAW
        /// </summary>
        /// <returns>LONG RAW</returns>
        [KeywordMethodConverter(Name = "LONG RAW")]
        public static IDataType LongRaw() => InvalitContext.Throw<IDataType>(nameof(LongRaw));

        /// <summary>
        /// LONGBLOB
        /// </summary>
        /// <returns>LONGBLOB</returns>
        [KeywordMethodConverter]
        public static IDataType LongBlob() => InvalitContext.Throw<IDataType>(nameof(LongBlob));

        /// <summary>
        /// LONGTEXT
        /// </summary>
        /// <returns>LONGTEXT</returns>
        [KeywordMethodConverter]
        public static IDataType LongText() => InvalitContext.Throw<IDataType>(nameof(LongText));

        /// <summary>
        /// LONGVARCHAR
        /// </summary>
        /// <returns>LONGVARCHAR</returns>
        [KeywordMethodConverter]
        public static IDataType LongVarchar() => InvalitContext.Throw<IDataType>(nameof(LongVarchar));

        /// <summary>
        /// LONGVARGRAPHIC
        /// </summary>
        /// <returns>LONGVARGRAPHIC</returns>
        [KeywordMethodConverter]
        public static IDataType LongVarGraphic() => InvalitContext.Throw<IDataType>(nameof(LongVarGraphic));

        /// <summary>
        /// LSEG
        /// </summary>
        /// <returns>LSEG</returns>
        [KeywordMethodConverter]
        public static IDataType Lseg() => InvalitContext.Throw<IDataType>(nameof(Lseg));

        /// <summary>
        /// MACADDR
        /// </summary>
        /// <returns>MACADDR</returns>
        [KeywordMethodConverter]
        public static IDataType MacAddr() => InvalitContext.Throw<IDataType>(nameof(MacAddr));

        /// <summary>
        /// MEDIUMBLOB
        /// </summary>
        /// <returns>MEDIUMBLOB</returns>
        [KeywordMethodConverter]
        public static IDataType MediumBlob() => InvalitContext.Throw<IDataType>(nameof(MediumBlob));

        /// <summary>
        /// MEDIUMINT
        /// </summary>
        /// <returns>MEDIUMINT</returns>
        [KeywordMethodConverter]
        public static IDataType MediumInt() => InvalitContext.Throw<IDataType>(nameof(MediumInt));

        /// <summary>
        /// MEDIUMTEXT
        /// </summary>
        /// <returns>MEDIUMTEXT</returns>
        [KeywordMethodConverter]
        public static IDataType MediumText() => InvalitContext.Throw<IDataType>(nameof(MediumText));

        /// <summary>
        /// MONEY
        /// </summary>
        /// <returns>MONEY</returns>
        [KeywordMethodConverter]
        public static IDataType Money() => InvalitContext.Throw<IDataType>(nameof(Money));

        /// <summary>
        /// NATIVE CHARACTER
        /// </summary>
        /// <returns>NATIVE CHARACTER</returns>
        [KeywordMethodConverter(Name = "NATIVE CHARACTER")]
        public static IDataType NativeCharacter() => InvalitContext.Throw<IDataType>(nameof(NativeCharacter));

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <returns>NCHAR</returns>
        [KeywordMethodConverter]
        public static IDataType NChar() => InvalitContext.Throw<IDataType>(nameof(NChar));

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NCHAR</returns>
        [FuncConverter]
        public static IDataType NChar(int n) => InvalitContext.Throw<IDataType>(nameof(NChar));

        /// <summary>
        /// NCLOB
        /// </summary>
        /// <returns>NCLOB</returns>
        [KeywordMethodConverter]
        public static IDataType NClob() => InvalitContext.Throw<IDataType>(nameof(NClob));

        /// <summary>
        /// NTEXT
        /// </summary>
        /// <returns>NTEXT</returns>
        [KeywordMethodConverter]
        public static IDataType NText() => InvalitContext.Throw<IDataType>(nameof(NText));

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <returns>NUMBER</returns>
        [KeywordMethodConverter]
        public static IDataType Number() => InvalitContext.Throw<IDataType>(nameof(Number));

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMBER</returns>
        [FuncConverter]
        public static IDataType Number(int precision) => InvalitContext.Throw<IDataType>(nameof(Number));

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMBER</returns>
        [FuncConverter]
        public static IDataType Number(int precision, int scale) => InvalitContext.Throw<IDataType>(nameof(Number));
        
        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <returns>NUMERIC</returns>
        [KeywordMethodConverter]
        public static IDataType Numeric() => InvalitContext.Throw<IDataType>(nameof(Numeric));

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMERIC</returns>
        [FuncConverter]
        public static IDataType Numeric(int precision) => InvalitContext.Throw<IDataType>(nameof(Numeric));

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMERIC</returns>
        [FuncConverter]
        public static IDataType Numeric(int precision, int scale) => InvalitContext.Throw<IDataType>(nameof(Numeric));

        /// <summary>
        /// NVARCHAR
        /// </summary>
        /// <returns>NVARCHAR</returns>
        [KeywordMethodConverter]
        public static IDataType NVarChar() => InvalitContext.Throw<IDataType>(nameof(NVarChar));

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <returns>NVARCHAR2</returns>
        [KeywordMethodConverter]
        public static IDataType NVarChar2() => InvalitContext.Throw<IDataType>(nameof(NVarChar2));

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NVARCHAR2</returns>
        [FuncConverter]
        public static IDataType NVarChar2(int n) => InvalitContext.Throw<IDataType>(nameof(NVarChar2));

        /// <summary>
        /// PATH
        /// </summary>
        /// <returns>PATH</returns>
        [KeywordMethodConverter]
        public static IDataType Path() => InvalitContext.Throw<IDataType>(nameof(Path));

        /// <summary>
        /// PG_LSN
        /// </summary>
        /// <returns>PG_LSN</returns>
        [KeywordMethodConverter]
        public static IDataType Pg_Lsn() => InvalitContext.Throw<IDataType>(nameof(Pg_Lsn));

        /// <summary>
        /// POINT
        /// </summary>
        /// <returns>POINT</returns>
        [KeywordMethodConverter]
        public static IDataType Point() => InvalitContext.Throw<IDataType>(nameof(Point));

        /// <summary>
        /// POLYGON
        /// </summary>
        /// <returns>POLYGON</returns>
        [KeywordMethodConverter]
        public static IDataType Polygon() => InvalitContext.Throw<IDataType>(nameof(Polygon));

        /// <summary>
        /// RAW
        /// </summary>
        /// <returns>RAW</returns>
        [KeywordMethodConverter]
        public static IDataType Raw() => InvalitContext.Throw<IDataType>(nameof(Raw));

        /// <summary>
        /// REAL
        /// </summary>
        /// <returns>REAL</returns>
        [KeywordMethodConverter]
        public static IDataType Real() => InvalitContext.Throw<IDataType>(nameof(Real));

        /// <summary>
        /// SERIAL
        /// </summary>
        /// <returns>SERIAL</returns>
        [KeywordMethodConverter]
        public static IDataType Serial() => InvalitContext.Throw<IDataType>(nameof(Serial));

        /// <summary>
        /// SET
        /// </summary>
        /// <returns>SET</returns>
        [KeywordMethodConverter]
        public static IDataType Set() => InvalitContext.Throw<IDataType>(nameof(Set));

        /// <summary>
        /// SMALLDATETIME
        /// </summary>
        /// <returns>SMALLDATETIME</returns>
        [KeywordMethodConverter]
        public static IDataType SmallDateTime() => InvalitContext.Throw<IDataType>(nameof(SmallDateTime));

        /// <summary>
        /// SMALLINT
        /// </summary>
        /// <returns>SMALLINT</returns>
        [KeywordMethodConverter]
        public static IDataType SmallInt() => InvalitContext.Throw<IDataType>(nameof(SmallInt));

        /// <summary>
        /// SMALLMONEY
        /// </summary>
        /// <returns>SMALLMONEY</returns>
        [KeywordMethodConverter]
        public static IDataType SmallMoney() => InvalitContext.Throw<IDataType>(nameof(SmallMoney));

        /// <summary>
        /// SMALLSERIAL
        /// </summary>
        /// <returns>SMALLSERIAL</returns>
        [KeywordMethodConverter]
        public static IDataType SmallSerial() => InvalitContext.Throw<IDataType>(nameof(SmallSerial));

        /// <summary>
        /// TEXT
        /// </summary>
        /// <returns>TEXT</returns>
        [KeywordMethodConverter]
        public static IDataType Text() => InvalitContext.Throw<IDataType>(nameof(Text));

        /// <summary>
        /// TIME
        /// </summary>
        /// <returns>TIME</returns>
        [KeywordMethodConverter]
        public static IDataType Time() => InvalitContext.Throw<IDataType>(nameof(Time));

        /// <summary>
        /// TIME
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [FuncConverter]
        public static IDataType Time(int n) => InvalitContext.Throw<IDataType>(nameof(Time));

        //TODO オラクルはTIMEZONE
        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <returns>TIME</returns>
        [KeywordMethodConverter(Name = "TIME WITH TIME ZONE")]
        public static IDataType TimeWithTimeZone() => InvalitContext.Throw<IDataType>(nameof(Time));

        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [FuncConverter(Name = "TIME", AfterPredicate = "WITH TIME ZONE")]
        public static IDataType TimeWithTimeZone(int n) => InvalitContext.Throw<IDataType>(nameof(Time));

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [KeywordMethodConverter]
        public static IDataType TimeStamp() => InvalitContext.Throw<IDataType>(nameof(TimeStamp));

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [FuncConverter]
        public static IDataType TimeStamp(int n) => InvalitContext.Throw<IDataType>(nameof(TimeStamp));

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [KeywordMethodConverter(Name = "TIME STAMP WITH TIME ZONE")]
        public static IDataType TimeStampWithTimeZone() => InvalitContext.Throw<IDataType>(nameof(TimeStamp));

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [FuncConverter(Name = "TIME STAMP", AfterPredicate = "WITH TIME ZONE")]
        public static IDataType TimeStampWithTimeZone(int n) => InvalitContext.Throw<IDataType>(nameof(TimeStamp));

        /// <summary>
        /// TINYBLOB
        /// </summary>
        /// <returns>TINYBLOB</returns>
        [KeywordMethodConverter]
        public static IDataType TinyBlob() => InvalitContext.Throw<IDataType>(nameof(TinyBlob));

        /// <summary>
        /// TINYINT
        /// </summary>
        /// <returns>TINYINT</returns>
        [KeywordMethodConverter]
        public static IDataType TinyInt() => InvalitContext.Throw<IDataType>(nameof(TinyInt));

        /// <summary>
        /// TINYTEXT
        /// </summary>
        /// <returns>TINYTEXT</returns>
        [KeywordMethodConverter]
        public static IDataType TinyText() => InvalitContext.Throw<IDataType>(nameof(TinyText));

        /// <summary>
        /// TSQUERY
        /// </summary>
        /// <returns>TSQUERY</returns>
        [KeywordMethodConverter]
        public static IDataType TsQuery() => InvalitContext.Throw<IDataType>(nameof(TsQuery));

        /// <summary>
        /// TSVECTOR
        /// </summary>
        /// <returns>TSVECTOR</returns>
        [KeywordMethodConverter]
        public static IDataType TsVector() => InvalitContext.Throw<IDataType>(nameof(TsVector));

        /// <summary>
        /// TXID_SNAPSHOT
        /// </summary>
        /// <returns>TXID_SNAPSHOT</returns>
        [KeywordMethodConverter]
        public static IDataType Txid_Snapshot() => InvalitContext.Throw<IDataType>(nameof(Txid_Snapshot));

        /// <summary>
        /// UNSIGNED BIG INT
        /// </summary>
        /// <returns>UNSIGNED BIG INT</returns>
        [KeywordMethodConverter(Name = "UNSIGNED BIG INT")]
        public static IDataType UnsignedBigInt() => InvalitContext.Throw<IDataType>(nameof(UnsignedBigInt));

        /// <summary>
        /// UUID
        /// </summary>
        /// <returns>UUID</returns>
        [KeywordMethodConverter]
        public static IDataType Uuid() => InvalitContext.Throw<IDataType>(nameof(Uuid));

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <returns>VARBINARY</returns>
        [KeywordMethodConverter]
        public static IDataType VarBinary() => InvalitContext.Throw<IDataType>(nameof(VarBinary));

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARBINARY</returns>
        [FuncConverter]
        public static IDataType VarBinary(int n) => InvalitContext.Throw<IDataType>(nameof(VarBinary));

        //TODO VARCHAR(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <returns>VARCHAR</returns>
        [KeywordMethodConverter]
        public static IDataType VarChar() => InvalitContext.Throw<IDataType>(nameof(VarChar));

        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR</returns>
        [FuncConverter]
        public static IDataType VarChar(int n) => InvalitContext.Throw<IDataType>(nameof(VarChar));

        //TODO VARCHAR2(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <returns>VARCHAR2</returns>
        [KeywordMethodConverter]
        public static IDataType VarChar2() => InvalitContext.Throw<IDataType>(nameof(VarChar2));

        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR2</returns>
        [FuncConverter]
        public static IDataType VarChar2(int n) => InvalitContext.Throw<IDataType>(nameof(VarChar2));

        /// <summary>
        /// VARGRAPHIC
        /// </summary>
        /// <returns>VARGRAPHIC</returns>
        [KeywordMethodConverter]
        public static IDataType VarGraphic() => InvalitContext.Throw<IDataType>(nameof(VarGraphic));

        /// <summary>
        /// VARYING CHARACTER
        /// </summary>
        /// <returns>VARYING CHARACTER</returns>
        [KeywordMethodConverter(Name = "VARYING CHARACTER")]
        public static IDataType VaryingCharacter() => InvalitContext.Throw<IDataType>(nameof(VaryingCharacter));

        /// <summary>
        /// XML
        /// </summary>
        /// <returns>XML</returns>
        [KeywordMethodConverter]
        public static IDataType Xml() => InvalitContext.Throw<IDataType>(nameof(Xml));

        /// <summary>
        /// XMLTYPE
        /// </summary>
        /// <returns>XMLTYPE</returns>
        [KeywordMethodConverter]
        public static IDataType XmlType() => InvalitContext.Throw<IDataType>(nameof(XmlType));

        /// <summary>
        /// YEAR
        /// </summary>
        /// <returns>YEAR</returns>
        [KeywordMethodConverter]
        public static IDataType Year() => InvalitContext.Throw<IDataType>(nameof(Year));
    }
}