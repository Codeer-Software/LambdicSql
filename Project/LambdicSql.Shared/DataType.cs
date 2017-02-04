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
        public static DataTypeElement BFile() { throw new InvalitContextException(nameof(BFile)); }

        /// <summary>
        /// BIGINT
        /// </summary>
        /// <returns>BIGINT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement BigInt() { throw new InvalitContextException(nameof(BigInt)); }

        /// <summary>
        /// BIGSERIAL
        /// </summary>
        /// <returns>BIGSERIAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement BigSerial() { throw new InvalitContextException(nameof(BigSerial)); }

        /// <summary>
        /// BINARY
        /// </summary>
        /// <returns>BINARY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Binary() { throw new InvalitContextException(nameof(Binary)); }

        /// <summary>
        /// BINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BINARY</returns>
        [FuncStyleConverter]
        public static DataTypeElement Binary(int n) { throw new InvalitContextException(nameof(Binary)); }

        /// <summary>
        /// BINARY_DOUBLE
        /// </summary>
        /// <returns>BINARY_DOUBLE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Binary_Double() { throw new InvalitContextException(nameof(Binary_Double)); }

        /// <summary>
        /// BINARY_FLOAT
        /// </summary>
        /// <returns>BINARY_FLOAT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Binary_Float() { throw new InvalitContextException(nameof(Binary_Float)); }

        /// <summary>
        /// BIT
        /// </summary>
        /// <returns>BIT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Bit() { throw new InvalitContextException(nameof(Bit)); }

        /// <summary>
        /// BIT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT</returns>
        [FuncStyleConverter]
        public static DataTypeElement Bit(int n) { throw new InvalitContextException(nameof(Bit)); }

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <returns>BIT VARYING</returns>
        [ClauseStyleConverter(Name = "BIT VARYING")]
        public static DataTypeElement BitVarying() { throw new InvalitContextException(nameof(BitVarying)); }

        /// <summary>
        /// BIT VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>BIT VARYING</returns>
        [FuncStyleConverter(Name = "BIT VARYING")]
        public static DataTypeElement BitVarying(int n) { throw new InvalitContextException(nameof(BitVarying)); }

        /// <summary>
        /// BLOB
        /// </summary>
        /// <returns>BLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Blob() { throw new InvalitContextException(nameof(Blob)); }

        /// <summary>
        /// BOOLEAN
        /// </summary>
        /// <returns>BOOLEAN</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Boolean() { throw new InvalitContextException(nameof(Boolean)); }

        /// <summary>
        /// BOX
        /// </summary>
        /// <returns>BOX</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Box() { throw new InvalitContextException(nameof(Box)); }

        /// <summary>
        /// BYTEA
        /// </summary>
        /// <returns>BYTEA</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Bytea() { throw new InvalitContextException(nameof(Bytea)); }

        /// <summary>
        /// CHAR
        /// </summary>
        /// <returns>CHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Char() { throw new InvalitContextException(nameof(Char)); }

        /// <summary>
        /// CHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement Char(int n) { throw new InvalitContextException(nameof(Char)); }

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <returns>CHARACTER</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Character() { throw new InvalitContextException(nameof(Character)); }

        /// <summary>
        /// CHARACTER
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER</returns>
        [FuncStyleConverter]
        public static DataTypeElement Character(int n) { throw new InvalitContextException(nameof(Character)); }

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <returns>CHARACTER VARYING</returns>
        [ClauseStyleConverter(Name = "CHARACTER VARYING")]
        public static DataTypeElement CharacterVarying() { throw new InvalitContextException(nameof(CharacterVarying)); }

        /// <summary>
        /// CHARACTER VARYING
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>CHARACTER VARYING</returns>
        [FuncStyleConverter(Name = "CHARACTER VARYING")]
        public static DataTypeElement CharacterVarying(int n) { throw new InvalitContextException(nameof(CharacterVarying)); }

        /// <summary>
        /// CIDR
        /// </summary>
        /// <returns>CIDR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Cidr() { throw new InvalitContextException(nameof(Cidr)); }

        /// <summary>
        /// CIRCLE
        /// </summary>
        /// <returns>CIRCLE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Circle() { throw new InvalitContextException(nameof(Circle)); }

        /// <summary>
        /// CLOB
        /// </summary>
        /// <returns>CLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Clob() { throw new InvalitContextException(nameof(Clob)); }

        /// <summary>
        /// CURRENCY
        /// </summary>
        /// <returns>CURRENCY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Currency() { throw new InvalitContextException(nameof(Currency)); }

        /// <summary>
        /// DATE
        /// </summary>
        /// <returns>DATE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Date() { throw new InvalitContextException(nameof(Date)); }

        /// <summary>
        /// DATETIME
        /// </summary>
        /// <returns>DATETIME</returns>
        [ClauseStyleConverter]
        public static DataTypeElement DateTime() { throw new InvalitContextException(nameof(DateTime)); }

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <returns>DATETIME2</returns>
        [ClauseStyleConverter]
        public static DataTypeElement DateTime2() { throw new InvalitContextException(nameof(DateTime2)); }

        /// <summary>
        /// DATETIME2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIME2</returns>
        [FuncStyleConverter]
        public static DataTypeElement DateTime2(int n) { throw new InvalitContextException(nameof(DateTime2)); }

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <returns>DATETIMEOFFSET</returns>
        [ClauseStyleConverter]
        public static DataTypeElement DateTimeOffset() { throw new InvalitContextException(nameof(DateTimeOffset)); }

        /// <summary>
        /// DATETIMEOFFSET
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>DATETIMEOFFSET</returns>
        [FuncStyleConverter]
        public static DataTypeElement DateTimeOffset(int n) { throw new InvalitContextException(nameof(DateTimeOffset)); }

        /// <summary>
        /// DBCLOB
        /// </summary>
        /// <returns>DBCLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement DbClob() { throw new InvalitContextException(nameof(DbClob)); }

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Decimal() { throw new InvalitContextException(nameof(Decimal)); }

        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Decimal(int precision) { throw new InvalitContextException(nameof(Decimal)); }


        /// <summary>
        /// DECIMAL
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>DECIMAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Decimal(int precision, int scale) { throw new InvalitContextException(nameof(Decimal)); }

        /// <summary>
        /// DOUBLE
        /// </summary>
        /// <returns>DOUBLE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Double() { throw new InvalitContextException(nameof(Double)); }

        /// <summary>
        /// DOUBLE PRECISION
        /// </summary>
        /// <returns>DOUBLE PRECISION</returns>
        [ClauseStyleConverter(Name = "DOUBLE PRECISION")]
        public static DataTypeElement DoublePrecision() { throw new InvalitContextException(nameof(DoublePrecision)); }

        /// <summary>
        /// ENUM
        /// </summary>
        /// <returns>ENUM</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Enum() { throw new InvalitContextException(nameof(Enum)); }

        /// <summary>
        /// ENUM
        /// </summary>
        /// <param name="elements">Elements</param>
        /// <returns>ENUM</returns>
        [FuncStyleConverter]
        public static DataTypeElement Enum(params object[] elements) { throw new InvalitContextException(nameof(Enum)); }

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <returns>FLOAT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Float() { throw new InvalitContextException(nameof(Float)); }

        /// <summary>
        /// FLOAT
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>FLOAT</returns>
        [FuncStyleConverter]
        public static DataTypeElement Float(int n) { throw new InvalitContextException(nameof(Float)); }

        /// <summary>
        /// GRAPHIC
        /// </summary>
        /// <returns>GRAPHIC</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Graphic() { throw new InvalitContextException(nameof(Graphic)); }

        /// <summary>
        /// IMAGE
        /// </summary>
        /// <returns>IMAGE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Image() { throw new InvalitContextException(nameof(Image)); }

        /// <summary>
        /// INET
        /// </summary>
        /// <returns>INET</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Inet() { throw new InvalitContextException(nameof(Inet)); }

        /// <summary>
        /// INT
        /// </summary>
        /// <returns>INT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Int() { throw new InvalitContextException(nameof(Int)); }

        /// <summary>
        /// INT2
        /// </summary>
        /// <returns>INT2</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Int2() { throw new InvalitContextException(nameof(Int2)); }

        /// <summary>
        /// INT8
        /// </summary>
        /// <returns>INT8</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Int8() { throw new InvalitContextException(nameof(Int8)); }

        /// <summary>
        /// INTEGER
        /// </summary>
        /// <returns>INTEGER</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Integer() { throw new InvalitContextException(nameof(Integer)); }

        /// <summary>
        /// INTERVAL
        /// </summary>
        /// <returns>INTERVAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Interval() { throw new InvalitContextException(nameof(Interval)); }

        /// <summary>
        /// INTERVAL.
        /// </summary>
        /// <param name="p">accuracy.</param>
        /// <returns>INTERVAL.</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Interval(int p) { throw new InvalitContextException(nameof(Interval)); }

        /// <summary>
        /// INTERVAL.
        /// </summary>
        /// <param name="type">type.</param>
        /// <returns>INTERVAL.</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Interval(IntervalType type) { throw new InvalitContextException(nameof(Interval)); }

        /// <summary>
        /// INTERVAL.
        /// </summary>
        /// <param name="type">type.</param>
        /// <param name="p">accuracy.</param>
        /// <returns>INTERVAL.</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Interval(IntervalType type, int p) { throw new InvalitContextException(nameof(Interval)); }

        /// <summary>
        /// JSON
        /// </summary>
        /// <returns>JSON</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Json() { throw new InvalitContextException(nameof(Json)); }

        /// <summary>
        /// JSONB
        /// </summary>
        /// <returns>JSONB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement JsonB() { throw new InvalitContextException(nameof(JsonB)); }

        /// <summary>
        /// LINE
        /// </summary>
        /// <returns>LINE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Line() { throw new InvalitContextException(nameof(Line)); }

        /// <summary>
        /// LONG
        /// </summary>
        /// <returns>LONG</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Long() { throw new InvalitContextException(nameof(Long)); }

        /// <summary>
        /// LONG RAW
        /// </summary>
        /// <returns>LONG RAW</returns>
        [ClauseStyleConverter(Name = "LONG RAW")]
        public static DataTypeElement LongRaw() { throw new InvalitContextException(nameof(LongRaw)); }

        /// <summary>
        /// LONGBLOB
        /// </summary>
        /// <returns>LONGBLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement LongBlob() { throw new InvalitContextException(nameof(LongBlob)); }

        /// <summary>
        /// LONGTEXT
        /// </summary>
        /// <returns>LONGTEXT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement LongText() { throw new InvalitContextException(nameof(LongText)); }

        /// <summary>
        /// LONGVARCHAR
        /// </summary>
        /// <returns>LONGVARCHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement LongVarchar() { throw new InvalitContextException(nameof(LongVarchar)); }

        /// <summary>
        /// LONGVARCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>LONGVARCHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement LongVarchar(int n) { throw new InvalitContextException(nameof(LongVarchar)); }

        /// <summary>
        /// LONGVARGRAPHIC
        /// </summary>
        /// <returns>LONGVARGRAPHIC</returns>
        [ClauseStyleConverter]
        public static DataTypeElement LongVarGraphic() { throw new InvalitContextException(nameof(LongVarGraphic)); }

        /// <summary>
        /// LSEG
        /// </summary>
        /// <returns>LSEG</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Lseg() { throw new InvalitContextException(nameof(Lseg)); }

        /// <summary>
        /// MACADDR
        /// </summary>
        /// <returns>MACADDR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement MacAddr() { throw new InvalitContextException(nameof(MacAddr)); }

        /// <summary>
        /// MEDIUMBLOB
        /// </summary>
        /// <returns>MEDIUMBLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement MediumBlob() { throw new InvalitContextException(nameof(MediumBlob)); }

        /// <summary>
        /// MEDIUMINT
        /// </summary>
        /// <returns>MEDIUMINT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement MediumInt() { throw new InvalitContextException(nameof(MediumInt)); }

        /// <summary>
        /// MEDIUMTEXT
        /// </summary>
        /// <returns>MEDIUMTEXT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement MediumText() { throw new InvalitContextException(nameof(MediumText)); }

        /// <summary>
        /// MONEY
        /// </summary>
        /// <returns>MONEY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Money() { throw new InvalitContextException(nameof(Money)); }

        /// <summary>
        /// NATIVE CHARACTER
        /// </summary>
        /// <returns>NATIVE CHARACTER</returns>
        [ClauseStyleConverter(Name = "NATIVE CHARACTER")]
        public static DataTypeElement NativeCharacter() { throw new InvalitContextException(nameof(NativeCharacter)); }

        /// <summary>
        /// NATIVE CHARACTER
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NATIVE CHARACTER</returns>
        [FuncStyleConverter(Name = "NATIVE CHARACTER")]
        public static DataTypeElement NativeCharacter(int n) { throw new InvalitContextException(nameof(NativeCharacter)); }

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <returns>NCHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement NChar() { throw new InvalitContextException(nameof(NChar)); }

        /// <summary>
        /// NCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NCHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement NChar(int n) { throw new InvalitContextException(nameof(NChar)); }

        /// <summary>
        /// NCLOB
        /// </summary>
        /// <returns>NCLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement NClob() { throw new InvalitContextException(nameof(NClob)); }

        /// <summary>
        /// NTEXT
        /// </summary>
        /// <returns>NTEXT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement NText() { throw new InvalitContextException(nameof(NText)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <returns>NUMBER</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Number() { throw new InvalitContextException(nameof(Number)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMBER</returns>
        [FuncStyleConverter]
        public static DataTypeElement Number(int precision) { throw new InvalitContextException(nameof(Number)); }

        /// <summary>
        /// NUMBER
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMBER</returns>
        [FuncStyleConverter]
        public static DataTypeElement Number(int precision, int scale) { throw new InvalitContextException(nameof(Number)); }
        
        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <returns>NUMERIC</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Numeric() { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <returns>NUMERIC</returns>
        [FuncStyleConverter]
        public static DataTypeElement Numeric(int precision) { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NUMERIC
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        /// <returns>NUMERIC</returns>
        [FuncStyleConverter]
        public static DataTypeElement Numeric(int precision, int scale) { throw new InvalitContextException(nameof(Numeric)); }

        /// <summary>
        /// NVARCHAR
        /// </summary>
        /// <returns>NVARCHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement NVarChar() { throw new InvalitContextException(nameof(NVarChar)); }

        /// <summary>
        /// NVARCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NVARCHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement NVarChar(int n) { throw new InvalitContextException(nameof(NVarChar)); }

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <returns>NVARCHAR2</returns>
        [ClauseStyleConverter]
        public static DataTypeElement NVarChar2() { throw new InvalitContextException(nameof(NVarChar2)); }

        /// <summary>
        /// NVARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>NVARCHAR2</returns>
        [FuncStyleConverter]
        public static DataTypeElement NVarChar2(int n) { throw new InvalitContextException(nameof(NVarChar2)); }

        /// <summary>
        /// PATH
        /// </summary>
        /// <returns>PATH</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Path() { throw new InvalitContextException(nameof(Path)); }

        /// <summary>
        /// PG_LSN
        /// </summary>
        /// <returns>PG_LSN</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Pg_Lsn() { throw new InvalitContextException(nameof(Pg_Lsn)); }

        /// <summary>
        /// POINT
        /// </summary>
        /// <returns>POINT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Point() { throw new InvalitContextException(nameof(Point)); }

        /// <summary>
        /// POLYGON
        /// </summary>
        /// <returns>POLYGON</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Polygon() { throw new InvalitContextException(nameof(Polygon)); }

        /// <summary>
        /// RAW
        /// </summary>
        /// <returns>RAW</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Raw() { throw new InvalitContextException(nameof(Raw)); }

        /// <summary>
        /// REAL
        /// </summary>
        /// <returns>REAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Real() { throw new InvalitContextException(nameof(Real)); }

        /// <summary>
        /// SERIAL
        /// </summary>
        /// <returns>SERIAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Serial() { throw new InvalitContextException(nameof(Serial)); }

        /// <summary>
        /// SET
        /// </summary>
        /// <returns>SET</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Set() { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// SET
        /// </summary>
        /// <param name="elements">Elements</param>
        /// <returns>SET</returns>
        [FuncStyleConverter]
        public static DataTypeElement Set(params object[] elements) { throw new InvalitContextException(nameof(Set)); }

        /// <summary>
        /// SMALLDATETIME
        /// </summary>
        /// <returns>SMALLDATETIME</returns>
        [ClauseStyleConverter]
        public static DataTypeElement SmallDateTime() { throw new InvalitContextException(nameof(SmallDateTime)); }

        /// <summary>
        /// SMALLINT
        /// </summary>
        /// <returns>SMALLINT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement SmallInt() { throw new InvalitContextException(nameof(SmallInt)); }

        /// <summary>
        /// SMALLMONEY
        /// </summary>
        /// <returns>SMALLMONEY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement SmallMoney() { throw new InvalitContextException(nameof(SmallMoney)); }

        /// <summary>
        /// SMALLSERIAL
        /// </summary>
        /// <returns>SMALLSERIAL</returns>
        [ClauseStyleConverter]
        public static DataTypeElement SmallSerial() { throw new InvalitContextException(nameof(SmallSerial)); }

        /// <summary>
        /// TEXT
        /// </summary>
        /// <returns>TEXT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Text() { throw new InvalitContextException(nameof(Text)); }

        /// <summary>
        /// TIME
        /// </summary>
        /// <returns>TIME</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Time() { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIME
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [FuncStyleConverter]
        public static DataTypeElement Time(int n) { throw new InvalitContextException(nameof(Time)); }
        
        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <returns>TIME</returns>
        [ClauseStyleConverter(Name = "TIME WITH TIME ZONE")]
        public static DataTypeElement TimeWithTimeZone() { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIME WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIME</returns>
        [MethodFormatConverter(Format = "TIME([0]) WITH TIME ZONE|")]
        public static DataTypeElement TimeWithTimeZone(int n) { throw new InvalitContextException(nameof(Time)); }

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TimeStamp() { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIMESTAMP
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [FuncStyleConverter]
        public static DataTypeElement TimeStamp(int n) { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <returns>TIMESTAMP</returns>
        [ClauseStyleConverter(Name = "TIMESTAMP WITH TIME ZONE")]
        public static DataTypeElement TimeStampWithTimeZone() { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TIME STAMP WITH TIME ZONE
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>TIMESTAMP</returns>
        [MethodFormatConverter(Format = "TIMESTAMP([0]) WITH TIME ZONE|")]
        public static DataTypeElement TimeStampWithTimeZone(int n) { throw new InvalitContextException(nameof(TimeStamp)); }

        /// <summary>
        /// TINYBLOB
        /// </summary>
        /// <returns>TINYBLOB</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TinyBlob() { throw new InvalitContextException(nameof(TinyBlob)); }

        /// <summary>
        /// TINYINT
        /// </summary>
        /// <returns>TINYINT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TinyInt() { throw new InvalitContextException(nameof(TinyInt)); }

        /// <summary>
        /// TINYTEXT
        /// </summary>
        /// <returns>TINYTEXT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TinyText() { throw new InvalitContextException(nameof(TinyText)); }

        /// <summary>
        /// TSQUERY
        /// </summary>
        /// <returns>TSQUERY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TsQuery() { throw new InvalitContextException(nameof(TsQuery)); }

        /// <summary>
        /// TSVECTOR
        /// </summary>
        /// <returns>TSVECTOR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement TsVector() { throw new InvalitContextException(nameof(TsVector)); }

        /// <summary>
        /// TXID_SNAPSHOT
        /// </summary>
        /// <returns>TXID_SNAPSHOT</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Txid_Snapshot() { throw new InvalitContextException(nameof(Txid_Snapshot)); }

        /// <summary>
        /// UNSIGNED BIG INT
        /// </summary>
        /// <returns>UNSIGNED BIG INT</returns>
        [ClauseStyleConverter(Name = "UNSIGNED BIG INT")]
        public static DataTypeElement UnsignedBigInt() { throw new InvalitContextException(nameof(UnsignedBigInt)); }

        /// <summary>
        /// UUID
        /// </summary>
        /// <returns>UUID</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Uuid() { throw new InvalitContextException(nameof(Uuid)); }

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <returns>VARBINARY</returns>
        [ClauseStyleConverter]
        public static DataTypeElement VarBinary() { throw new InvalitContextException(nameof(VarBinary)); }

        /// <summary>
        /// VARBINARY
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARBINARY</returns>
        [FuncStyleConverter]
        public static DataTypeElement VarBinary(int n) { throw new InvalitContextException(nameof(VarBinary)); }

        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <returns>VARCHAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement VarChar() { throw new InvalitContextException(nameof(VarChar)); }

        /// <summary>
        /// VARCHAR
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR</returns>
        [FuncStyleConverter]
        public static DataTypeElement VarChar(int n) { throw new InvalitContextException(nameof(VarChar)); }

        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <returns>VARCHAR2</returns>
        [ClauseStyleConverter]
        public static DataTypeElement VarChar2() { throw new InvalitContextException(nameof(VarChar2)); }

        /// <summary>
        /// VARCHAR2
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARCHAR2</returns>
        [FuncStyleConverter]
        public static DataTypeElement VarChar2(int n) { throw new InvalitContextException(nameof(VarChar2)); }

        /// <summary>
        /// VARGRAPHIC
        /// </summary>
        /// <returns>VARGRAPHIC</returns>
        [ClauseStyleConverter]
        public static DataTypeElement VarGraphic() { throw new InvalitContextException(nameof(VarGraphic)); }

        /// <summary>
        /// VARYING CHARACTER
        /// </summary>
        /// <returns>VARYING CHARACTER</returns>
        [ClauseStyleConverter(Name = "VARYING CHARACTER")]
        public static DataTypeElement VaryingCharacter() { throw new InvalitContextException(nameof(VaryingCharacter)); }

        /// <summary>
        /// VARYING CHARACTER
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>VARYING CHARACTER</returns>
        [FuncStyleConverter(Name = "VARYING CHARACTER")]
        public static DataTypeElement VaryingCharacter(int n) { throw new InvalitContextException(nameof(VaryingCharacter)); }

        /// <summary>
        /// XML
        /// </summary>
        /// <returns>XML</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Xml() { throw new InvalitContextException(nameof(Xml)); }

        /// <summary>
        /// XMLTYPE
        /// </summary>
        /// <returns>XMLTYPE</returns>
        [ClauseStyleConverter]
        public static DataTypeElement XmlType() { throw new InvalitContextException(nameof(XmlType)); }

        /// <summary>
        /// YEAR
        /// </summary>
        /// <returns>YEAR</returns>
        [ClauseStyleConverter]
        public static DataTypeElement Year() { throw new InvalitContextException(nameof(Year)); }
    }
}
