using System.Data.Entity;

namespace StandardTemplate.Test
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MyFirstTestModel : DbContext
    {
        /* $mark_context$ */
        //public virtual DbSet<Alarm> Alarms { get; set; }
        public virtual DbSet<Valve2> valve2s { get; set; }

        public MyFirstTestModel() : base("name=LocalConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyFirstTestModel, StandardTemplate.Migrations.Configuration>());
            //Database.SetInitializer<LocalDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}