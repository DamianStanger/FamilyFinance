using System;

namespace FamilyFinance.Models.ViewModel
{
    public class StatementOverviewViewModel
    {
        public int AccountId { get; set; }
        public double In { get; set; }
        public double Out { get; set; }
        public double Amount { get; set; }
        public string StatementDate { get; set; }
        public DateTime Date { get; set; }
    }
}