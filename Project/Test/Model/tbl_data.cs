namespace Test.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_data
    {
        [StringLength(10)]
        public string id { get; set; }

        public int? val1 { get; set; }

        [StringLength(10)]
        public string val2 { get; set; }
    }
}
