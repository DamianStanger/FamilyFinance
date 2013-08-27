using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public HomeController() : this(new AccountRepository(), new TransactionRepository())
        {
        }

        public HomeController(IAccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }


        public ActionResult Index()
        {
            var viewModel = new HomeIndex();
            ViewBag.Message = "Account Overview";

            var accounts = accountRepository.All;
            foreach (var account in accounts)
            {
                var transactions = transactionRepository.All.Where(x => x.AccountId == account.Id);
                var sum = 0d;
                if (transactions.Any())
                {
                    sum = transactions.Sum(x => x.Amount);
                    
                }
                var accountViewModel = new AccountViewModel() {balance = sum, name = account.Name, Id = account.Id};
                viewModel.Accounts.Add(accountViewModel);
            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
