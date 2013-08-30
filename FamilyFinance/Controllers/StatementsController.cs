using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Controllers
{
    public class StatementsController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatementsController() : this(new AccountRepository(), new TransactionRepository())
        {
        }

        public StatementsController(IAccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }


        public ActionResult Statement(int accountId, int year, int month)
        {
            var account = accountRepository.Find(accountId);
            var date = new DateTime(year, month, 1).ToLongDateString().Replace("01 ", "");
            var viewModel = new StatementViewModel
                {
                    AccountName = account.Name,
                    StatementDate = date,
                    Transactions =
                        transactionRepository.All.Where(
                            x => x.AccountId == accountId && x.Date.Year == year && x.Date.Month == month)
                };
            return View(viewModel);
        }
    }

    public class StatementViewModel
    {
        public StatementViewModel()
        {
        }

        public string AccountName { get; set; }
        public IQueryable<FamilyFinance.Models.Domain.Transaction> Transactions { get; set; }
        public string StatementDate { get; set; }
    }
}
