using System;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.ViewModel
{
    public class AccountActivityViewModel
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double RunningTotal { get; set; }

        public AccountActivityViewModel(Transaction transaction, double runningTotal)
        {
            Date = transaction.Date;
            Category = transaction.Category.Name;
            Account = transaction.Account.Name;
            Amount = transaction.Amount;
            Description = transaction.Description;
            Name = transaction.Account.Name;
            RunningTotal = runningTotal;
        }
    }
}