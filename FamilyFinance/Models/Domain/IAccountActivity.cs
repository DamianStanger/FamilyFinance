using System;

namespace FamilyFinance.Models.Domain
{
    public interface IAccountActivity
    {
        int Id { get; set; }
        string Name { get; set; }
        double Amount { get; set; }
        string Description { get; set; }
        DateTime Date { get; set; }
        int InitiatorId { get; set; }
        Person Initiator { get; set; }
        int AccountId { get; set; }
        Account Account { get; set; }
        int CategoryId { get; set; }
        Category Category { get; set; }
    }
}