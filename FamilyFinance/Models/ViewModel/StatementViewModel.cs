using System.Collections.Generic;
using System.Linq;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.ViewModel
{
    public class StatementViewModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string StatementDate { get; set; }
        public int PreviousYear { get; set; }
        public int PreviousMonth { get; set; }
        public int NextYear { get; set; }
        public int NextMonth { get; set; }

        public double MoneyIn { get; set; }
        public double MoneyOut { get; set; }
    }
}