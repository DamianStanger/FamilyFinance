using System;
using System.Data.Entity;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.Repository
{
    public class FamilyFinanceInitialiser : DropCreateDatabaseIfModelChanges<FamilyFinanceContext>
    {
        private Account DamoBarclaysCurrent;
        private Account JointBarcalys;
        private Account DamoBarclaysSavings;
        private Account CaraBarclaysSavings;
        private Account DamoLloydsCurrent;
        private Account DamoLloydsIsa;
        private Account DamoBarclaycard;
        private Account JointBarclaycard;
        private Account DamoCash;
        private Account KatCash;
        private Category initialDepositCategory;
        private Person damo;
        private Person kat;
        private Person cara;

        protected override void Seed(FamilyFinanceContext context)
        {
            base.Seed(context);

            damo = new Person {Name = "Damo"};
            kat = new Person {Name = "Kat"};
            cara = new Person {Name = "Cara"};
            context.People.Add(damo);
            context.People.Add(kat);
            context.People.Add(cara);

            var debit = new AccountType() {Name = "Debit", Id = AccountType.Debit};
            var savings = new AccountType() { Name = "Savings", Id = AccountType.Savings };
            var credit = new AccountType() { Name = "Credit", Id = AccountType.Credit };
            var loan = new AccountType() { Name = "Loan", Id = AccountType.Loan };
            var cash = new AccountType() { Name = "Cash", Id = AccountType.Cash };
            context.AccountTypes.Add(debit);
            context.AccountTypes.Add(savings);
            context.AccountTypes.Add(credit);
            context.AccountTypes.Add(loan);
            context.AccountTypes.Add(cash);

            DamoBarclaysCurrent = new Account { Bank = "Barclays", Name = "Damo Current", Owner = damo, AccountType = debit};
            JointBarcalys = new Account { Bank = "Barclays", Name = "Joint Current", Owner = damo, AccountType = debit };
            DamoBarclaysSavings = new Account { Bank = "Barclays", Name = "Damo Savings", Owner = damo, AccountType = savings };
            CaraBarclaysSavings = new Account { Bank = "Barclays", Name = "Cara Savings", Owner = kat, AccountType = savings };
            DamoLloydsCurrent = new Account { Bank = "Lloyds", Name = "Damo Curent 2", Owner = damo, AccountType = debit };
            DamoLloydsIsa = new Account { Bank = "Lloyds", Name = "Damo ISA", Owner = damo, AccountType = savings };
            DamoBarclaycard = new Account { Bank = "Barclaycard", Name = "Damo Barclaycard", Owner = damo, AccountType = credit };
            JointBarclaycard = new Account { Bank = "Barclaycard", Name = "Joint Barclaycard", Owner = damo, AccountType = credit };
            DamoCash = new Account { Bank = "Cash", Name = "Damo Cash", Owner = damo, AccountType = cash };
            KatCash = new Account { Bank = "Cash", Name = "Kat Cash", Owner = kat, AccountType = cash };
            context.Accounts.Add(DamoBarclaysCurrent);
            context.Accounts.Add(JointBarcalys);
            context.Accounts.Add(DamoBarclaysSavings);
            context.Accounts.Add(CaraBarclaysSavings);
            context.Accounts.Add(DamoLloydsCurrent);
            context.Accounts.Add(DamoLloydsIsa);
            context.Accounts.Add(DamoBarclaycard);
            context.Accounts.Add(JointBarclaycard);
            context.Accounts.Add(DamoCash);
            context.Accounts.Add(KatCash);

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
            context.Categories.Add(travel);
            context.Categories.Add(entertainment);
            context.Categories.Add(car);

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

            var deposit = new Category {Name = "Deposit"};
            context.Categories.Add(deposit);
            initialDepositCategory = new Category {Name = "Initial Deposit", ParentCategory = deposit};
            context.Categories.Add(initialDepositCategory);

            context.SaveChanges();

            InitialDeposits(context);
        }

        private void InitialDeposits(FamilyFinanceContext context)
        {
            context.Transactions.Add(new Transaction()
            {
                Account = DamoBarclaysCurrent,
                Amount = 2000d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoBarclaysCurrent,
                Amount = 1300d,
                Category = initialDepositCategory,
                Date = DateTime.Now.AddMonths(-1),
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = JointBarcalys,
                Amount = 1500d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoBarclaysSavings,
                Amount = 457.78d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoLloydsCurrent,
                Amount = 134.98d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoLloydsIsa,
                Amount = 53d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = CaraBarclaysSavings,
                Amount = 980d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoBarclaycard,
                Amount = -400d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = JointBarclaycard,
                Amount = -790.65d,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "initial Deposit",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = DamoCash,
                Amount = 55,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "Cash in pocket",
                Initiator = damo
            });

            context.Transactions.Add(new Transaction()
            {
                Account = KatCash,
                Amount = 23,
                Category = initialDepositCategory,
                Date = DateTime.Now,
                Name = "Cash in pocket",
                Initiator = damo
            });
        }
    }
}