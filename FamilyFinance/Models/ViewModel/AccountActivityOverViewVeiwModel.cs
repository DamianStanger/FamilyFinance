using System.Collections.Generic;

namespace FamilyFinance.Models.ViewModel
{
    public class AccountActivityOverViewVeiwModel
    {
        public List<AccountActivitiesViewModel> Activities { get; set; }

        public double MoneyIn { get; set; }
        public double MoneyOut { get; set; }

        public AccountActivityOverViewVeiwModel(List<AccountActivitiesViewModel> activities)
        {
            Activities = activities;
        }
    }
}