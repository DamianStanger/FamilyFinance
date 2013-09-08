using System.Collections.Generic;
using FamilyFinance.Controllers;

namespace FamilyFinance.Models.ViewModel
{
    public class StatementsViewModel
    {
        public string AccountName { get; set; }
        public List<StatementOverviewViewModel> Statements { get; set; }
        public double MoneyIn { get; set; }
        public double MoneyOut { get; set; }
    }
}