using System;

namespace FamilyFinance.Models.Finance
{
    public class Transaction
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
    }
}