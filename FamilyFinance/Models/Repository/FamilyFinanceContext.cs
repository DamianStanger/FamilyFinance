using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.Repository
{
    public class FamilyFinanceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FamilyFinance.Models.FamilyFinanceContext>());

        public DbSet<Person> People { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        //remove all the cascading deletes
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<FamilyFinance.Models.Domain.AccountType> AccountTypes { get; set; }
    }
}