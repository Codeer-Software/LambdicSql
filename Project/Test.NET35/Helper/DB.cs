using System;

namespace Test
{
    public class Staff
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Remuneration
    {
        public int id { get; set; }
        public int staff_id { get; set; }
        public DateTime payment_date { get; set; }
        public decimal money { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public int val1 { get; set; }
        public string val2 { get; set; }
    }

    public class DB
    {
        public Staff tbl_staff { get; set; }
        public Remuneration tbl_remuneration { get; set; }
        public Data tbl_data { get; set; }
    }

    public class DBSchema
    {
        public DB dbo { get; set; }
    }

    public class Table1
    {
        public int id { get; set; }
        public int val1 { get; set; }
        public char[] val2 { get; set; }
    }
    public class Table2
    {
        public int id { get; set; }
        public int table1Id { get; set; }
        public int val1 { get; set; }
    }

    public class DBForCreateTest
    {
        public Table1 table1 { get; set; }
        public Table2 table2 { get; set; }
    }
}
