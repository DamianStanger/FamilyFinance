using System;

namespace FamilyFinance.Models.Domain
{
    public class Transaction : IAccountActivity
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public int InitiatorId { get; set; }
        public virtual Person Initiator { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}