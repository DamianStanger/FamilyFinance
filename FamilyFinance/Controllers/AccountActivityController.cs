using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class AccountActivityController : Controller
    {

        private readonly ITransactionRepository transactionRepository;
        private readonly ITransferRepository transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountActivityController() : this(new TransactionRepository(), new TransferRepository())
        {
        }

        public AccountActivityController(TransactionRepository transactionRepository, TransferRepository transferRepository)
        {
            this.transactionRepository = transactionRepository;
            this.transferRepository = transferRepository;
        }

        //
        // GET: /AccountActivity/

        public ActionResult Index()
        {
            var transactions = transactionRepository.All;
            var transactionActivities = new List<AccountActivitiesViewModel>();
            foreach (var transaction in transactions)
            {
                transactionActivities.Add(mapToAccountActivity(transaction));
            }
//            List<AccountActivitiesViewModel> transactionActivities = transactions.Select(x => mapToAccountActivity(x)).ToList();

            var transfers = transferRepository.All;
            var transferActivities = new List<AccountActivitiesViewModel>();
            foreach (var transfer in transfers)
            {
                transferActivities.Add(mapToAccountActivity(transfer));
            }
//            var transferActivities = transfers.Select(x => mapToAccountActivity(x)).ToList();

            var AllActivities = transactionActivities.Concat(transferActivities).OrderBy(x => x.Date);

            var runningTotal = 0d;
            foreach (var activity in AllActivities)
            {
                if (!activity.IsTransfer)
                {
                    runningTotal += activity.Amount;
                }
                activity.RunningTotal = runningTotal;
            }

            var accountActivityOverViewVeiwModel = new AccountActivityOverViewVeiwModel(AllActivities)
                {
                    MoneyIn = AllActivities.Sum(x => x.Amount > 0 ? x.Amount : 0),
                    MoneyOut = AllActivities.Sum(x => x.Amount < 0 ? x.Amount : 0)
                };

            return View(accountActivityOverViewVeiwModel);
        }

        private AccountActivitiesViewModel mapToAccountActivity(IAccountActivity accountActivity)
        {
            return new AccountActivitiesViewModel(accountActivity);
        }
    }
}
