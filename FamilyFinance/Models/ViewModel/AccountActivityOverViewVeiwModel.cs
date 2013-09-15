using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyFinance.Models.ViewModel
{
    public class AccountActivityOverViewVeiwModel
    {
        public List<AccountActivitiesViewModel> Activities { get; set; }

        public double MoneyIn { get; set; }
        public double MoneyOut { get; set; }

        public string StatementDate { get; set; }

        public int PreviousYear { get; set; }
        public int PreviousMonth { get; set; }
        public int NextYear { get; set; }
        public int NextMonth { get; set; }

        public AccountActivityOverViewVeiwModel(IEnumerable<AccountActivitiesViewModel> activities)
        {
            Activities = activities.ToList();
        }
    }
}