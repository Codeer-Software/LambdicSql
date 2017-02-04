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

    public class Table3
    {
        public object obj1 { get; set; }
        public object obj2 { get; set; }
        public object obj3 { get; set; }
        public object obj4 { get; set; }
        public object obj5 { get; set; }
        public object obj6 { get; set; }
        public object obj7 { get; set; }
        public object obj8 { get; set; }
        public object obj9 { get; set; }
        public object obj10 { get; set; }
        public object obj11 { get; set; }
        public object obj12 { get; set; }
        public object obj13 { get; set; }
        public object obj14 { get; set; }
        public object obj15 { get; set; }
        public object obj16 { get; set; }
        public object obj17 { get; set; }
        public object obj18 { get; set; }
        public object obj19 { get; set; }
        public object obj20 { get; set; }
        public object obj21 { get; set; }
        public object obj22 { get; set; }
        public object obj23 { get; set; }
        public object obj24 { get; set; }
        public object obj25 { get; set; }
        public object obj26 { get; set; }
        public object obj27 { get; set; }
        public object obj28 { get; set; }
        public object obj29 { get; set; }
        public object obj30 { get; set; }
        public object obj31 { get; set; }
        public object obj32 { get; set; }
        public object obj33 { get; set; }
        public object obj34 { get; set; }
        public object obj35 { get; set; }
        public object obj36 { get; set; }
        public object obj37 { get; set; }
        public object obj38 { get; set; }
        public object obj39 { get; set; }
        public object obj40 { get; set; }
        public object obj41 { get; set; }
        public object obj42 { get; set; }
        public object obj43 { get; set; }
        public object obj44 { get; set; }
        public object obj45 { get; set; }
        public object obj46 { get; set; }
        public object obj47 { get; set; }
        public object obj48 { get; set; }
        public object obj49 { get; set; }
        public object obj50 { get; set; }
    }

    public class DBForCreateTest
    {
        public Table1 table1 { get; set; }
        public Table2 table2 { get; set; }
        public Table3 table3 { get; set; }
    }
}
