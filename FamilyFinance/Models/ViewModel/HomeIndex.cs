using System.Collections.Generic;
namespace FamilyFinance.Models.ViewModel
{
    public class HomeIndex
    {
        public HomeIndex()
        {
            Accounts = new List<AccountViewModel>();
        }

        public IList<AccountViewModel> Accounts { get; private set; } 
    }
}