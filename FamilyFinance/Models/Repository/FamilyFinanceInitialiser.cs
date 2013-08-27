using System.Data.Entity;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.Repository
{
    public class FamilyFinanceInitialiser : DropCreateDatabaseAlways<FamilyFinanceContext>
    {
        protected override void Seed(FamilyFinanceContext context)
        {
            base.Seed(context);

            var damo = new Person {Name = "Damo"};
            var kat = new Person {Name = "Kat"};
            var cara = new Person {Name = "Cara"};
            context.People.Add(damo);
            context.People.Add(kat);
            context.People.Add(cara);

            context.Accounts.Add(new Account {Bank = "Barclays", Name = "Damo Current", Owner = damo});
            context.Accounts.Add(new Account { Bank = "Barclays", Name = "Joint Current", Owner = damo });
            context.Accounts.Add(new Account { Bank = "Barclays", Name = "Damo Savings", Owner = damo });
            context.Accounts.Add(new Account { Bank = "Barclays", Name = "Cara Savings", Owner = kat });
            context.Accounts.Add(new Account { Bank = "Lloyds", Name = "Damo Curent 2", Owner = damo });
            context.Accounts.Add(new Account { Bank = "Lloyds", Name = "Damo ISA", Owner = damo });
            context.Accounts.Add(new Account { Bank = "Barclaycard", Name = "Damo Barclaycard", Owner = damo });
            context.Accounts.Add(new Account { Bank = "Barclaycard", Name = "Joint Barclaycard", Owner = damo });

            context.SaveChanges();

            var bills = new Category() {Name = "Bills"};
            var house = new Category() {Name = "House"};
            var income = new Category() {Name = "Income"};
            var travel = new Category() {Name = "Travel"};
            var entertainment = new Category() {Name = "Entertainment"};
            var car = new Category() {Name = "Car"};
            context.Categories.Add(bills);
            context.Categories.Add(house);
            context.Categories.Add(income);

            context.Categories.Add(new Category { Name = "Train", ParentCategory = travel });
            context.Categories.Add(new Category { Name = "Lunch", ParentCategory = house });
            context.Categories.Add(new Category { Name = "Insurance", ParentCategory = car });
            context.Categories.Add(new Category { Name = "RAC", ParentCategory = car });
            context.Categories.Add(new Category { Name = "Petrol", ParentCategory = car });
            context.Categories.Add(new Category { Name = "MOT", ParentCategory = car });
            context.Categories.Add(new Category { Name = "Service", ParentCategory = car });
            context.Categories.Add(new Category { Name = "Cara", ParentCategory = house });
            context.Categories.Add(new Category { Name = "Shopping", ParentCategory = house });
            context.Categories.Add(new Category { Name = "Food", ParentCategory = house});
            context.Categories.Add(new Category { Name = "Ingenie", ParentCategory = income });
            context.Categories.Add(new Category { Name = "GreenSphere", ParentCategory = income });
            context.Categories.Add(new Category { Name = "Gas", ParentCategory = bills});
            context.Categories.Add(new Category { Name = "Elec", ParentCategory = bills });
            context.Categories.Add(new Category { Name = "Water", ParentCategory = bills });
            context.Categories.Add(new Category { Name = "CouncilTax", ParentCategory = bills });
            context.Categories.Add(new Category { Name = "Insurance", ParentCategory = bills });
            context.Categories.Add(new Category { Name = "Phone", ParentCategory = bills });
            context.Categories.Add(new Category { Name = "Internet", ParentCategory = bills });
            
            context.SaveChanges();
        }

    }
}