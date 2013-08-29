using System.Collections.Generic;
namespace FamilyFinance.Models.ViewModel
{
    public class HomeIndex
    {
        public HomeIndex()
        {
            Accounts = new Accounts();
            Totals = new Totals();
        }

        public Accounts Accounts { get; private set; }
        public Totals Totals { get; private set; }
    }

    public class Accounts 
    {
        public Accounts()
        {
            Debit = new List<AccountViewModel>();
            Credit = new List<AccountViewModel>();
            Savings = new List<AccountViewModel>();
            Loan = new List<AccountViewModel>();
        }

        public IList<AccountViewModel> Debit { get; set; }
        public IList<AccountViewModel> Credit { get; set; }
        public IList<AccountViewModel> Savings { get; set; }
        public IList<AccountViewModel> Loan { get; set; }
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