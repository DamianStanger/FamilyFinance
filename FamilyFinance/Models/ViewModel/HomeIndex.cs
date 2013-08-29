using System.Collections.Generic;
namespace FamilyFinance.Models.ViewModel
{
    public class HomeIndex
    {
        public HomeIndex()
        {
            Accounts = new List<AccountViewModel>();
            Totals = new Totals();
        }

        public IList<AccountViewModel> Accounts { get; private set; }
        public Totals Totals { get; set; }
    }

    public class Totals
    {
        public double Total { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Savings { get; set; }
        public double Loan { get; set; }
    }
}