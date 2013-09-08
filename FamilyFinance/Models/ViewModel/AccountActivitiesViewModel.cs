using System;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.ViewModel
{
    public class AccountActivitiesViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double RunningTotal { get; set; }
        public bool IsTransfer { get; set; }

        public AccountActivitiesViewModel(IAccountActivity accountActivity)
        {
            Id = accountActivity.Id;
            Date = accountActivity.Date;
            Category = accountActivity.Category.Name;
            Account = accountActivity.Account.Name;
            Amount = accountActivity.Amount;
            Description = accountActivity.Description;
            Name = accountActivity.Account.Name;
            IsTransfer = accountActivity is Transfer;
        }
    }
}