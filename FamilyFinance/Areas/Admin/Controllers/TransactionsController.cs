using System.Web.Mvc;
using FamilyFinance.Models;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class TransactionsController : Controller
    {
		private readonly IAccountRepository accountRepository;
		private readonly IPersonRepository personRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly ITransactionRepository transactionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TransactionsController() : this(new AccountRepository(), new PersonRepository(), new CategoryRepository(), new TransactionRepository())
        {
        }

        public TransactionsController(IAccountRepository accountRepository, IPersonRepository personRepository, ICategoryRepository categoryRepository, ITransactionRepository transactionRepository)
        {
			this.accountRepository = accountRepository;
			this.personRepository = personRepository;
			this.categoryRepository = categoryRepository;
			this.transactionRepository = transactionRepository;
        }

        //
        // GET: /Transactions/

        public ViewResult Index()
        {
            return View(transactionRepository.AllIncluding(transaction => transaction.Account, transaction => transaction.Initiator, transaction => transaction.Category));
        }

        //
        // GET: /Transactions/Details/5

        public ViewResult Details(int id)
        {
            return View(transactionRepository.Find(id));
        }

        //
        // GET: /Transactions/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAccounts = accountRepository.All;
			ViewBag.PossibleInitiators = personRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        } 

        //
        // POST: /Transactions/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid) {
                transactionRepository.InsertOrUpdate(transaction);
                transactionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccounts = accountRepository.All;
				ViewBag.PossibleInitiators = personRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Transactions/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAccounts = accountRepository.All;
			ViewBag.PossibleInitiators = personRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
             return View(transactionRepository.Find(id));
        }

        //
        // POST: /Transactions/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid) {
                transactionRepository.InsertOrUpdate(transaction);
                transactionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAccounts = accountRepository.All;
				ViewBag.PossibleInitiators = personRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /Transactions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(transactionRepository.Find(id));
        }

        //
        // POST: /Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            transactionRepository.Delete(id);
            transactionRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                accountRepository.Dispose();
                personRepository.Dispose();
                categoryRepository.Dispose();
                transactionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

