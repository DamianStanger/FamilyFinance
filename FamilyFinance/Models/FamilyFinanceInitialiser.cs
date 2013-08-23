using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

            context.Accounts.Add(new Account {Bank = "Barclays", Name = "Damo Current"});
            context.Accounts.Add(new Account {Bank = "Barclays", Name = "Joint Current"});
            context.Accounts.Add(new Account {Bank = "Barclays", Name = "Damo Savings"});
            context.Accounts.Add(new Account {Bank = "Barclays", Name = "Cara Savings"});
            context.Accounts.Add(new Account {Bank = "Lloyds", Name = "Damo Curent 2"});
            context.Accounts.Add(new Account {Bank = "Lloyds", Name = "Damo ISA"});
            context.Accounts.Add(new Account {Bank = "Barclaycard", Name = "Damo Barclaycard"});
            context.Accounts.Add(new Account {Bank = "Barclaycard", Name = "Joint Barclaycard"});

            context.Categories.Add(new Category {Name = "Travel"});
            context.Categories.Add(new Category {Name = "Lunch"});
            context.Categories.Add(new Category {Name = "Car"});
            context.Categories.Add(new Category {Name = "Cara"});
            context.Categories.Add(new Category {Name = "Shopping"});
            context.Categories.Add(new Category {Name = "Food"});
            context.Categories.Add(new Category {Name = "Ingenie"});
            context.Categories.Add(new Category {Name = "GreenSphere"});
            context.Categories.Add(new Category {Name = "Gas"});
            context.Categories.Add(new Category {Name = "Elec"});
            context.Categories.Add(new Category {Name = "Water"});
            context.Categories.Add(new Category {Name = "CouncilTax"});
            context.Categories.Add(new Category {Name = "Insurance"});
            context.Categories.Add(new Category {Name = "Phone"});
            
            context.SaveChanges();
        }

    }
}