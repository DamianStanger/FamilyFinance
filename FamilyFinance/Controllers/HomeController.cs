using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
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
            var viewModel = new HomeIndexViewModel();
            ViewBag.Message = "Account Overview";

            var accounts = accountRepository.All;
            
            foreach (var account in accounts)
            {
                var transactions = transactionRepository.All.Where(x => x.AccountId == account.Id);
                if (transactions.Any())
                {
                    var sum = transactions.Sum(x => x.Amount);
                    AccountViewModel accountViewModel;
                    switch (account.AccountTypeId)
                    {
                        case AccountType.Debit:
                            viewModel.Totals.Debit += sum;
                            accountViewModel = new AccountViewModel() { Balance = sum, Name = account.Name, Id = account.Id };
                            viewModel.Accounts.Debit.Add(accountViewModel);
                            break;
                        case AccountType.Credit:
                            viewModel.Totals.Credit += sum;
                            accountViewModel = new AccountViewModel() { Balance = sum, Name = account.Name, Id = account.Id };
                            viewModel.Accounts.Credit.Add(accountViewModel);
                            break;
                        case AccountType.Savings:
                            viewModel.Totals.Savings += sum;
                            accountViewModel = new AccountViewModel() { Balance = sum, Name = account.Name, Id = account.Id };
                            viewModel.Accounts.Savings.Add(accountViewModel);
                            break;
                        case AccountType.Loan:
                            viewModel.Totals.Loan += sum;
                            accountViewModel = new AccountViewModel() { Balance = sum, Name = account.Name, Id = account.Id };
                            viewModel.Accounts.Loan.Add(accountViewModel);
                            break;
                        case AccountType.Cash:
                            viewModel.Totals.Cash += sum;
                            accountViewModel = new AccountViewModel() { Balance = sum, Name = account.Name, Id = account.Id };
                            viewModel.Accounts.Cash.Add(accountViewModel);
                            break;
                    }
                    viewModel.Totals.Total += sum;
                }                
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
