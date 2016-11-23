namespace Test.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_remuneration
    {
        public int? staff_id { get; set; }

        [StringLength(10)]
        public string payment_date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(TypeName = "money")]
        public decimal? money { get; set; }
    }
}
