using SQLite.CodeFirst;
using System.Data.Entity;

namespace StandardTemplate.Test
{
    class DBInitializer:SqliteCreateDatabaseIfNotExists<MyFirstTestModel>
    {
        public DBInitializer(string connectionString, DbModelBuilder modelBuilder):base(modelBuilder)
        {

        }

        protected override void Seed(MyFirstTestModel context)
        {
            base.Seed(context);
        }
    }
}
