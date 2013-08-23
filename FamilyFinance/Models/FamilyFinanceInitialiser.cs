using System.Data.Entity;
using FamilyFinance.Models.Finance;

namespace FamilyFinance.Models
{
    public class FamilyFinanceInitialiser : DropCreateDatabaseAlways<FamilyFinanceContext>
    {
        protected override void Seed(FamilyFinanceContext context)
        {
            base.Seed(context);

            context.People.Add(new Person{Name = "Damo"});
            context.People.Add(new Person{Name = "Kat"});
            context.People.Add(new Person{Name = "Cara"});
            context.SaveChanges();
        }
    }
}