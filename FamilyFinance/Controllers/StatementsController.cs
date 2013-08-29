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
            var transactions = transactionRepository.All.Where(x => x.AccountId == accountId && x.Date.Year == year && x.Date.Month == month);
            return View(transactions);
        }
    }
}
