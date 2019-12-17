namespace StandardTemplate.Migrations
{
    using System.Data.Entity.Migrations;
    using Test;

    internal sealed class Configuration : DbMigrationsConfiguration<StandardTemplate.Test.MyFirstTestModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StandardTemplate.Test.MyFirstTestModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            /* $mark_configuration$ */
            //context.MyEntities.AddOrUpdate(new DemoEntity() { Name = "Tomy-1" });

            context.SaveChanges();
        }
    }
}
