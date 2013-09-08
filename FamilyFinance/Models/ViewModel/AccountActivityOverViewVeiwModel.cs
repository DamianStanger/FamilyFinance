using System.Collections.Generic;
using System.Linq;

namespace FamilyFinance.Models.ViewModel
{
    public class AccountActivityOverViewVeiwModel
    {
        public List<AccountActivitiesViewModel> Activities { get; set; }

        public double MoneyIn { get; set; }
        public double MoneyOut { get; set; }

        public AccountActivityOverViewVeiwModel(IEnumerable<AccountActivitiesViewModel> activities)
        {
            Activities = activities.ToList();
        }
    }
}