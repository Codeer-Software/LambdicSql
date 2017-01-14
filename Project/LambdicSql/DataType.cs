using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// Data type.
    /// </summary>
    public static class DataType
    {
        /// <summary>
        /// BFILE
        /// </summary>
        /// <returns>BFILE</returns>
        [ClauseStyleConverter]
        public static IDataType BFile() { throw new InvalitContextException(nameof(BFile)); }

        /// <summary>
        /// BIGINT
        /// </summary>
        /// <returns>BIGINT</returns>
        [ClauseStyleConverter]
        public static IDataType BigInt() { throw new InvalitContextException(nameof(BigInt)); }

        /// <summary>
        /// BIGSERIAL
        /// </summary>
        /// <returns>BIGSERIAL</returns>
        [ClauseStyleConverter]
        public static IDataType BigSerial() { throw new InvalitContextException(nameof(BigSerial)); }

        /// <summary>
        /// BINARY
        /// </summary>
        /// <returns>BINARY</returns>
        [ClauseStyleConverter]
        public static IDataType Binary() { throw new InvalitContextException(nameof(Binary)); }

        /// <summary>
        /// BINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BINARY</returns>
        [FuncStyleConverter]
        public static IDataType Binary(int n) { throw new InvalitContextException(nameof(Binary)); }

        /// <summary>
        /// BINARY_DOUBLE
        /// </summary>
        /// <returns>BINARY_DOUBLE</returns>
        [ClauseStyleConverter]
        public static IDataType Binary_Double() { throw new InvalitContextException(nameof(Binary_Double)); }

        /// <summary>
        /// BINARY_FLOAT
        /// </summary>
        /// <returns>BINARY_FLOAT</returns>
        [ClauseStyleConverter]
        public static IDataType Binary_Float() { throw new InvalitContextException(nameof(Binary_Float)); }

        /// <summary>
        /// BIT
        /// </summary>
        /// <returns>BIT</returns>
        [ClauseStyleConverter]
        public static IDataType Bit() { throw new InvalitContextException(nameof(Bit)); }

        /// <summary>
        /// BIT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT</returns>
        [FuncStyleConverter]
        public static IDataType Bit(int n) { throw new InvalitContextException(nameof(Bit)); }

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <returns>BIT VARYING</returns>
        [ClauseStyleConverter(Name = "BIT VARYING")]
        public static IDataType BitVarying() { throw new InvalitContextException(nameof(BitVarying)); }

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT VARYING</returns>
        [FuncStyleConverter(Name = "BIT VARYING")]
        public static IDataType BitVarying(int n) { throw new InvalitContextException(nameof(BitVarying)); }

        /// <summary>
        /// BLOB
        /// </summary>
        /// <returns>BLOB</returns>
        [ClauseStyleConverter]
        public static IDataType Blob() { throw new InvalitContextException(nameof(Blob)); }

        /// <summary>
        /// BOOLEAN
        /// </summary>
        /// <returns>BOOLEAN</returns>
        [ClauseStyleConverter]
        public static IDataType Boolean() { throw new InvalitContextException(nameof(Boolean)); }

        /// <summary>
        /// BOX
        /// </summary>
        /// <returns>BOX</returns>
        [ClauseStyleConverter]
        public static IDataType Box() { throw new InvalitContextException(nameof(Box)); }

        /// <summary>
        /// BYTEA
        /// </summary>
        /// <returns>BYTEA</returns>
        [ClauseStyleConverter]
        public static IDataType Bytea() { throw new InvalitContextException(nameof(Bytea)); }

        //TODO CHAR(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// CHAR
        /// </summary>
        /// <returns>CHAR</returns>
        [ClauseStyleConverter]
        public static IDataType Char() { throw new InvalitContextException(nameof(Char)); }

        /// <summary>
        /// CHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHAR</returns>
        [FuncStyleConverter]
        public static IDataType Char(int n) { throw new InvalitContextException(nameof(Char)); }

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <returns>CHARACTER</returns>
        [ClauseStyleConverter]
        public static IDataType Character() { throw new InvalitContextException(nameof(Character)); }

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER</returns>
        [FuncStyleConverter]
        public static IDataType Character(int n) { throw new InvalitContextException(nameof(Character)); }

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <returns>CHARACTER VARYING</returns>
        [ClauseStyleConverter(Name = "CHARACTER VARYING")]
        public static IDataType CharacterVarying() { throw new InvalitContextException(nameof(CharacterVarying)); }

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER VARYING</returns>
        [FuncStyleConverter(Name = "CHARACTER VARYING")]
        public static IDataType CharacterVarying(int n) { throw new InvalitContextException(nameof(CharacterVarying)); }

        /// <summary>
        /// CIDR
        /// </summary>
        /// <returns>CIDR</returns>
        [ClauseStyleConverter]
        public static IDataType Cidr() { throw new InvalitContextException(nameof(Cidr)); }

        /// <summary>
        /// CIRCLE
        /// </summary>
        /// <returns>CIRCLE</returns>
        [ClauseStyleConverter]
        public static IDataType Circle() { throw new InvalitContextException(nameof(Circle)); }

        /// <summary>
        /// CLOB
        /// </summary>
        /// <returns>CLOB</returns>
        [ClauseStyleConverter]
        public static IDataType Clob() { throw new InvalitContextException(nameof(Clob)); }

        /// <summary>
        /// CURRENCY
        /// </summary>
        /// <returns>CURRENCY</returns>
        [ClauseStyleConverter]
        public static IDataType Currency() { throw new InvalitContextException(nameof(Currency)); }

        /// <summary>
        /// DATE
        /// </summary>
        /// <returns>DATE</returns>
        [ClauseStyleConverter]
        public static IDataType Date() { throw new InvalitContextException(nameof(Date)); }

        /// <summary>
        /// DATETIME
        /// </summary>
        /// <returns>DATETIME</returns>
        [ClauseStyleConverter]
        public static IDataType DateTime() { throw new InvalitContextException(nameof(DateTime)); }

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <returns>DATETIME2</returns>
        [ClauseStyleConverter]
        public static IDataType DateTime2() { throw new InvalitContextException(nameof(DateTime2)); }

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIME2</returns>
        [FuncStyleConverter]
        public static IDataType DateTime2(int n) { throw new InvalitContextException(nameof(DateTime2)); }

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <returns>DATETIMEOFFSET</returns>
        [ClauseStyleConverter]
        public static IDataType DateTimeOffset() { throw new InvalitContextException(nameof(DateTimeOffset)); }

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIMEOFFSET</returns>
        [FuncStyleConverter]
        public static IDataType DateTimeOffset(int n) { throw new InvalitContextException(nameof(DateTimeOffset)); }

        /// <summary>
        /// DBCLOB
        /// </summary>
        /// <returns>DBCLOB</returns>
        [ClauseStyleConverter]
        public static IDataType DbClob() { throw new InvalitContextException(nameof(DbClob)); }

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static IDataType Decimal() { throw new InvalitContextException(nameof(Decimal)); }

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static IDataType Decimal(int precision) { throw new InvalitContextException(nameof(Decimal)); }


        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static IDataType Decimal(int precision, int scale) { throw new InvalitContextException(nameof(Decimal)); }

        /// <summary>
        /// DOUBLE
        /// </summary>
        /// <returns>DOUBLE</returns>
        [ClauseStyleConverter]
        public static IDataType Double() { throw new InvalitContextException(nameof(Double)); }

        /// <summary>
        /// DOUBLE PRECISION
        /// </summary>
        /// <returns>DOUBLE PRECISION</returns>
        [ClauseStyleConverter(Name = "DOUBLE PRECISION")]
        public static IDataType DoublePrecision() { throw new InvalitContextException(nameof(DoublePrecision)); }

        /// <summary>
        /// ENUM
        /// </summary>
        /// <returns>ENUM</returns>
        [ClauseStyleConverter]
        public static IDataType Enum() { throw new InvalitContextException(nameof(Enum)); }

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <returns>FLOAT</returns>
        [ClauseStyleConverter]
        public static IDataType Float() { throw new InvalitContextException(nameof(Float)); }

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>FLOAT</returns>
        [FuncStyleConverter]
        public static IDataType Float(int n) { throw new InvalitContextException(nameof(Float)); }

        /// <summary>
        /// GRAPHIC
        /// </summary>
        /// <returns>GRAPHIC</returns>
        [ClauseStyleConverter]
        public static IDataType Graphic() { throw new InvalitContextException(nameof(Graphic)); }

        /// <summary>
        /// IMAGE
        /// </summary>
        /// <returns>IMAGE</returns>
        [ClauseStyleConverter]
        public static IDataType Image() { throw new InvalitContextException(nameof(Image)); }

        /// <summary>
        /// INET
        /// </summary>
        /// <returns>INET</returns>
        [ClauseStyleConverter]
        public static IDataType Inet() { throw new InvalitContextException(nameof(Inet)); }

        /// <summary>
        /// INT
        /// </summary>
        /// <returns>INT</returns>
        [ClauseStyleConverter]
        public static IDataType Int() { throw new InvalitContextException(nameof(Int)); }

        /// <summary>
        /// INT2
        /// </summary>
        /// <returns>INT2</returns>
        [ClauseStyleConverter]
        public static IDataType Int2() { throw new InvalitContextException(nameof(Int2)); }

        /// <summary>
        /// INT8
        /// </summary>
        /// <returns>INT8</returns>
        [ClauseStyleConverter]
        public static IDataType Int8() { throw new InvalitContextException(nameof(Int8)); }

        /// <summary>
        /// INTEGER
        /// </summary>
        /// <returns>INTEGER</returns>
        [ClauseStyleConverter]
        public static IDataType Integer() { throw new InvalitContextException(nameof(Integer)); }

        //TODO interval [ fields ] [ (p) ] fieldはenumにするか
        //TODO オラクル考慮
        /// <summary>
        /// INTERVAL
        /// </summary>
        /// <returns>INTERVAL</returns>
        [ClauseStyleConverter]
        public static IDataType Interval() { throw new InvalitContextException(nameof(Interval)); }

        /// <summary>
        /// JSON
        /// </summary>
        /// <returns>JSON</returns>
        [ClauseStyleConverter]
        public static IDataType Json() { throw new InvalitContextException(nameof(Json)); }

        /// <summary>
        /// JSONB
        /// </summary>
        /// <returns>JSONB</returns>
        [ClauseStyleConverter]
        public static IDataType JsonB() { throw new InvalitContextException(nameof(JsonB)); }

        /// <summary>
        /// LINE
        /// </summary>
        /// <returns>LINE</returns>
        [ClauseStyleConverter]
        public static IDataType Line() { throw new InvalitContextException(nameof(Line)); }

        /// <summary>
        /// LONG
        /// </summary>
        /// <returns>LONG</returns>
        [ClauseStyleConverter]
        public static IDataType Long() { throw new InvalitContextException(nameof(Long)); }

        /// <summary>
        /// LONG RAW
        /// </summary>
        /// <returns>LONG RAW</returns>
        [ClauseStyleConverter(Name = "LONG RAW")]
        public static IDataType LongRaw() { throw new InvalitContextException(nameof(LongRaw)); }

        /// <summary>
        /// LONGBLOB
        /// </summary>
        /// <returns>LONGBLOB</returns>
        [ClauseStyleConverter]
        public static IDataType LongBlob() { throw new InvalitContextException(nameof(LongBlob)); }

        /// <summary>
        /// LONGTEXT
        /// </summary>
        /// <returns>LONGTEXT</returns>
        [ClauseStyleConverter]
        public static IDataType LongText() { throw new InvalitContextException(nameof(LongText)); }

        /// <summary>
        /// LONGVARCHAR
        /// </summary>
        /// <returns>LONGVARCHAR</returns>
        [ClauseStyleConverter]
        public static IDataType LongVarchar() { throw new InvalitContextException(nameof(LongVarchar)); }

        /// <summary>
        /// LONGVARGRAPHIC
        /// </summary>
        /// <returns>LONGVARGRAPHIC</returns>
        [ClauseStyleConverter]
        public static IDataType LongVarGraphic() { throw new InvalitContextException(nameof(LongVarGraphic)); }

        /// <summary>
        /// LSEG
        /// </summary>
        /// <returns>LSEG</returns>
        [ClauseStyleConverter]
        public static IDataType Lseg() { throw new InvalitContextException(nameof(Lseg)); }

        /// <summary>
        /// MACADDR
        /// </summary>
        /// <returns>MACADDR</returns>
        [ClauseStyleConverter]
        public static IDataType MacAddr() { throw new InvalitContextException(nameof(MacAddr)); }

        /// <summary>
        /// MEDIUMBLOB
        /// </summary>
        /// <returns>MEDIUMBLOB</returns>
        [ClauseStyleConverter]
        public static IDataType MediumBlob() { throw new InvalitContextException(nameof(MediumBlob)); }

        /// <summary>
        /// MEDIUMINT
        /// </summary>
        /// <returns>MEDIUMINT</returns>
        [ClauseStyleConverter]
        public static IDataType MediumInt() { throw new InvalitContextException(nameof(MediumInt)); }

        /// <summary>
        /// MEDIUMTEXT
        /// </summary>
        /// <returns>MEDIUMTEXT</returns>
        [ClauseStyleConverter]
        public static IDataType MediumText() { throw new InvalitContextException(nameof(MediumText)); }

        /// <summary>
        /// MONEY
        /// </summary>
        /// <returns>MONEY</returns>
        [ClauseStyleConverter]
        public static IDataType Money() { throw new InvalitContextException(nameof(Money)); }

        /// <summary>
        /// NATIVE CHARACTER
        /// </summary>
        /// <returns>NATIVE CHARACTER</returns>
        [ClauseStyleConverter(Name = "NATIVE CHARACTER")]
        public static IDataType NativeCharacter() { throw new InvalitContextException(nameof(NativeCharacter)); }

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <returns>NCHAR</returns>
        [ClauseStyleConverter]
        public static IDataType NChar() { throw new InvalitContextException(nameof(NChar)); }

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NCHAR</returns>
        [FuncStyleConverter]
        public static IDataType NChar(int n) { throw new InvalitContextException(nameof(NChar)); }

        /// <summary>
        /// NCLOB
        /// </summary>
        /// <returns>NCLOB</returns>
        [ClauseStyleConverter]
        public static IDataType NClob() { throw new InvalitContextException(nameof(NClob)); }

        /// <summary>
        /// NTEXT
        /// </summary>
        /// <returns>NTEXT</returns>
        [ClauseStyleConverter]
        public static IDataType NText() { throw new InvalitContextException(nameof(NText)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <returns>NUMBER</returns>
        [ClauseStyleConverter]
        public static IDataType Number() { throw new InvalitContextException(nameof(Number)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMBER</returns>
        [FuncStyleConverter]
        public static IDataType Number(int precision) { throw new InvalitContextException(nameof(Number)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMBER</returns>
        [FuncStyleConverter]
        public static IDataType Number(int precision, int scale) { throw new InvalitContextException(nameof(Number)); }
        
        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <returns>NUMERIC</returns>
        [ClauseStyleConverter]
        public static IDataType Numeric() { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMERIC</returns>
        [FuncStyleConverter]
        public static IDataType Numeric(int precision) { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMERIC</returns>
        [FuncStyleConverter]
        public static IDataType Numeric(int precision, int scale) { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NVARCHAR
        /// </summary>
        /// <returns>NVARCHAR</returns>
        [ClauseStyleConverter]
        public static IDataType NVarChar() { throw new InvalitContextException(nameof(NVarChar)); }

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <returns>NVARCHAR2</returns>
        [ClauseStyleConverter]
        public static IDataType NVarChar2() { throw new InvalitContextException(nameof(NVarChar2)); }

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NVARCHAR2</returns>
        [FuncStyleConverter]
        public static IDataType NVarChar2(int n) { throw new InvalitContextException(nameof(NVarChar2)); }

        /// <summary>
        /// PATH
        /// </summary>
        /// <returns>PATH</returns>
        [ClauseStyleConverter]
        public static IDataType Path() { throw new InvalitContextException(nameof(Path)); }

        /// <summary>
        /// PG_LSN
        /// </summary>
        /// <returns>PG_LSN</returns>
        [ClauseStyleConverter]
        public static IDataType Pg_Lsn() { throw new InvalitContextException(nameof(Pg_Lsn)); }

        /// <summary>
        /// POINT
        /// </summary>
        /// <returns>POINT</returns>
        [ClauseStyleConverter]
        public static IDataType Point() { throw new InvalitContextException(nameof(Point)); }

        /// <summary>
        /// POLYGON
        /// </summary>
        /// <returns>POLYGON</returns>
        [ClauseStyleConverter]
        public static IDataType Polygon() { throw new InvalitContextException(nameof(Polygon)); }

        /// <summary>
        /// RAW
        /// </summary>
        /// <returns>RAW</returns>
        [ClauseStyleConverter]
        public static IDataType Raw() { throw new InvalitContextException(nameof(Raw)); }

        /// <summary>
        /// REAL
        /// </summary>
        /// <returns>REAL</returns>
        [ClauseStyleConverter]
        public static IDataType Real() { throw new InvalitContextException(nameof(Real)); }

        /// <summary>
        /// SERIAL
        /// </summary>
        /// <returns>SERIAL</returns>
        [ClauseStyleConverter]
        public static IDataType Serial() { throw new InvalitContextException(nameof(Serial)); }

        /// <summary>
        /// SET
        /// </summary>
        /// <returns>SET</returns>
        [ClauseStyleConverter]
        public static IDataType Set() { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// SMALLDATETIME
        /// </summary>
        /// <returns>SMALLDATETIME</returns>
        [ClauseStyleConverter]
        public static IDataType SmallDateTime() { throw new InvalitContextException(nameof(SmallDateTime)); }

        /// <summary>
        /// SMALLINT
        /// </summary>
        /// <returns>SMALLINT</returns>
        [ClauseStyleConverter]
        public static IDataType SmallInt() { throw new InvalitContextException(nameof(SmallInt)); }

        /// <summary>
        /// SMALLMONEY
        /// </summary>
        /// <returns>SMALLMONEY</returns>
        [ClauseStyleConverter]
        public static IDataType SmallMoney() { throw new InvalitContextException(nameof(SmallMoney)); }

        /// <summary>
        /// SMALLSERIAL
        /// </summary>
        /// <returns>SMALLSERIAL</returns>
        [ClauseStyleConverter]
        public static IDataType SmallSerial() { throw new InvalitContextException(nameof(SmallSerial)); }

        /// <summary>
        /// TEXT
        /// </summary>
        /// <returns>TEXT</returns>
        [ClauseStyleConverter]
        public static IDataType Text() { throw new InvalitContextException(nameof(Text)); }

        /// <summary>
        /// TIME
        /// </summary>
        /// <returns>TIME</returns>
        [ClauseStyleConverter]
        public static IDataType Time() { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIME
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [FuncStyleConverter]
        public static IDataType Time(int n) { throw new InvalitContextException(nameof(Time)); }

        //TODO オラクルはTIMEZONE
        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <returns>TIME</returns>
        [ClauseStyleConverter(Name = "TIME WITH TIME ZONE")]
        public static IDataType TimeWithTimeZone() { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [MethodFormatConverter(Format = "TIME([0]) WITH TIME ZONE|")]
        public static IDataType TimeWithTimeZone(int n) { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [ClauseStyleConverter]
        public static IDataType TimeStamp() { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [FuncStyleConverter]
        public static IDataType TimeStamp(int n) { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [ClauseStyleConverter(Name = "TIME STAMP WITH TIME ZONE")]
        public static IDataType TimeStampWithTimeZone() { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [MethodFormatConverter(Format = "TIME STAMP([0]) WITH TIME ZONE|")]
        public static IDataType TimeStampWithTimeZone(int n) { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TINYBLOB
        /// </summary>
        /// <returns>TINYBLOB</returns>
        [ClauseStyleConverter]
        public static IDataType TinyBlob() { throw new InvalitContextException(nameof(TinyBlob)); }

        /// <summary>
        /// TINYINT
        /// </summary>
        /// <returns>TINYINT</returns>
        [ClauseStyleConverter]
        public static IDataType TinyInt() { throw new InvalitContextException(nameof(TinyInt)); }

        /// <summary>
        /// TINYTEXT
        /// </summary>
        /// <returns>TINYTEXT</returns>
        [ClauseStyleConverter]
        public static IDataType TinyText() { throw new InvalitContextException(nameof(TinyText)); }

        /// <summary>
        /// TSQUERY
        /// </summary>
        /// <returns>TSQUERY</returns>
        [ClauseStyleConverter]
        public static IDataType TsQuery() { throw new InvalitContextException(nameof(TsQuery)); }

        /// <summary>
        /// TSVECTOR
        /// </summary>
        /// <returns>TSVECTOR</returns>
        [ClauseStyleConverter]
        public static IDataType TsVector() { throw new InvalitContextException(nameof(TsVector)); }

        /// <summary>
        /// TXID_SNAPSHOT
        /// </summary>
        /// <returns>TXID_SNAPSHOT</returns>
        [ClauseStyleConverter]
        public static IDataType Txid_Snapshot() { throw new InvalitContextException(nameof(Txid_Snapshot)); }

        /// <summary>
        /// UNSIGNED BIG INT
        /// </summary>
        /// <returns>UNSIGNED BIG INT</returns>
        [ClauseStyleConverter(Name = "UNSIGNED BIG INT")]
        public static IDataType UnsignedBigInt() { throw new InvalitContextException(nameof(UnsignedBigInt)); }

        /// <summary>
        /// UUID
        /// </summary>
        /// <returns>UUID</returns>
        [ClauseStyleConverter]
        public static IDataType Uuid() { throw new InvalitContextException(nameof(Uuid)); }

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <returns>VARBINARY</returns>
        [ClauseStyleConverter]
        public static IDataType VarBinary() { throw new InvalitContextException(nameof(VarBinary)); }

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARBINARY</returns>
        [FuncStyleConverter]
        public static IDataType VarBinary(int n) { throw new InvalitContextException(nameof(VarBinary)); }

        //TODO VARCHAR(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <returns>VARCHAR</returns>
        [ClauseStyleConverter]
        public static IDataType VarChar() { throw new InvalitContextException(nameof(VarChar)); }

        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR</returns>
        [FuncStyleConverter]
        public static IDataType VarChar(int n) { throw new InvalitContextException(nameof(VarChar)); }

        //TODO VARCHAR2(maxlen CHAR) BYTE enumかな・・・
        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <returns>VARCHAR2</returns>
        [ClauseStyleConverter]
        public static IDataType VarChar2() { throw new InvalitContextException(nameof(VarChar2)); }

        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR2</returns>
        [FuncStyleConverter]
        public static IDataType VarChar2(int n) { throw new InvalitContextException(nameof(VarChar2)); }

        /// <summary>
        /// VARGRAPHIC
        /// </summary>
        /// <returns>VARGRAPHIC</returns>
        [ClauseStyleConverter]
        public static IDataType VarGraphic() { throw new InvalitContextException(nameof(VarGraphic)); }

        /// <summary>
        /// VARYING CHARACTER
        /// </summary>
        /// <returns>VARYING CHARACTER</returns>
        [ClauseStyleConverter(Name = "VARYING CHARACTER")]
        public static IDataType VaryingCharacter() { throw new InvalitContextException(nameof(VaryingCharacter)); }

        /// <summary>
        /// XML
        /// </summary>
        /// <returns>XML</returns>
        [ClauseStyleConverter]
        public static IDataType Xml() { throw new InvalitContextException(nameof(Xml)); }

        /// <summary>
        /// XMLTYPE
        /// </summary>
        /// <returns>XMLTYPE</returns>
        [ClauseStyleConverter]
        public static IDataType XmlType() { throw new InvalitContextException(nameof(XmlType)); }

        /// <summary>
        /// YEAR
        /// </summary>
        /// <returns>YEAR</returns>
        [ClauseStyleConverter]
        public static IDataType Year() { throw new InvalitContextException(nameof(Year)); }
    }
}
