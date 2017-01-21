using System;

namespace TestPlc
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

    public class DBEX
    {
        public DB dbo { get; set; }
    }
}
