namespace Test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TestCheck35;

    public partial class ModelLambdicSqlTestDB : DbContext
    {
        public ModelLambdicSqlTestDB()
            : base(TestEnvironment.EFConnectionString)
        {
        }

        public virtual DbSet<tbl_data> tbl_data { get; set; }
        public virtual DbSet<tbl_remuneration> tbl_remuneration { get; set; }
        public virtual DbSet<tbl_staff> tbl_staff { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_data>()
                .Property(e => e.id)
                .IsFixedLength();

            modelBuilder.Entity<tbl_data>()
                .Property(e => e.val2)
                .IsFixedLength();

            modelBuilder.Entity<tbl_remuneration>()
                .Property(e => e.payment_date)
                .IsFixedLength();

            modelBuilder.Entity<tbl_remuneration>()
                .Property(e => e.money)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbl_staff>()
                .Property(e => e.name)
                .IsUnicode(false);
        }
    }
}
