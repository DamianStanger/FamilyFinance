using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class AccountActivityController : Controller
    {

        private readonly ITransactionRepository transactionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountActivityController() : this(new TransactionRepository())
        {
        }

        public AccountActivityController(TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        //
        // GET: /AccountActivity/

        public ActionResult Index()
        {
            var transactions = transactionRepository.All.OrderBy(x => x.Date);
            var viewModel = new List<AccountActivityViewModel>();
            double runningTotal = 0d;
            foreach (var transaction in transactions)
            {
                runningTotal += transaction.Amount;
                var activity = new AccountActivityViewModel(transaction, runningTotal);
                viewModel.Add(activity);
            }
            viewModel.Reverse();
            return View(viewModel);
        }
    }
}
