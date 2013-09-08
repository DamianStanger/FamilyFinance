using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class AccountActivityController : Controller
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransferRepository _transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountActivityController() : this(new TransactionRepository(), new TransferRepository())
        {
        }

        public AccountActivityController(ITransactionRepository transactionRepository, ITransferRepository transferRepository)
        {
            this._transactionRepository = transactionRepository;
            this._transferRepository = transferRepository;
        }

        public ActionResult Index()
        {
            var transactionActivities = GetTransactionActivities();
            var transferActivities = GetTransferActivities();
            var allActivities = transactionActivities.Concat(transferActivities).OrderBy(x => x.Date);

            var runningTotal = 0d;
            foreach (var activity in allActivities)
            {
                if (!activity.IsTransfer)
                {
                    runningTotal += activity.Amount;
                }
                activity.RunningTotal = runningTotal;
            }

            var accountActivityOverViewVeiwModel = new AccountActivityOverViewVeiwModel(allActivities)
                {
                    MoneyIn = allActivities.Sum(x => x.Amount > 0 ? x.Amount : 0),
                    MoneyOut = allActivities.Sum(x => x.Amount < 0 ? x.Amount : 0)
                };

            return View(accountActivityOverViewVeiwModel);
        }

        private IEnumerable<AccountActivitiesViewModel> GetTransferActivities()
        {
            var transfers = _transferRepository.All;
            var transferActivities = new List<AccountActivitiesViewModel>();
            foreach (var transfer in transfers)
            {
                transferActivities.Add(mapToAccountActivity(transfer));
            }
            return transferActivities;
        }

        private IEnumerable<AccountActivitiesViewModel> GetTransactionActivities()
        {
            var transactions = _transactionRepository.All;
            var transactionActivities = new List<AccountActivitiesViewModel>();
            foreach (var transaction in transactions)
            {
                transactionActivities.Add(mapToAccountActivity(transaction));
            }
            return transactionActivities;
        }

        private AccountActivitiesViewModel mapToAccountActivity(IAccountActivity accountActivity)
        {
            return new AccountActivitiesViewModel(accountActivity);
        }
    }
}
