using System;

namespace FamilyFinance.Models.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Bank { get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}