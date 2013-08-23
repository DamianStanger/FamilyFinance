using System;

namespace FamilyFinance.Models.Finance
{
    public class Account
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Bank { get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }
    }
}