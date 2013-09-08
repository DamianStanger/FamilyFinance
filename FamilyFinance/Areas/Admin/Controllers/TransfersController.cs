using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class TransfersController : Controller
    {
		private readonly IPersonRepository personRepository;
		private readonly IAccountRepository accountRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly ITransferRepository transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TransfersController() : this(new PersonRepository(), new AccountRepository(), new CategoryRepository(), new TransferRepository())
        {
        }

        public TransfersController(IPersonRepository personRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, ITransferRepository transferRepository)
        {
			this.personRepository = personRepository;
			this.accountRepository = accountRepository;
			this.categoryRepository = categoryRepository;
			this.transferRepository = transferRepository;
        }

        //
        // GET: /Transfers/

        public ViewResult Index()
        {
            return View(transferRepository.AllIncluding(transfer => transfer.Initiator, transfer => transfer.Account, transfer => transfer.ToAccount, transfer => transfer.Category));
        }

        //
        // GET: /Transfers/Details/5

        public ViewResult Details(int id)
        {
            return View(transferRepository.Find(id));
        }

        //
        // GET: /Transfers/Create

        public ActionResult Create()
        {
			ViewBag.PossibleInitiators = personRepository.All;
			ViewBag.PossibleAccounts = accountRepository.All;
			ViewBag.PossibleToAccounts = accountRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        } 

        //
        // POST: /Transfers/Create

        [HttpPost]
        public ActionResult Create(Transfer transfer)
        {
            if (ModelState.IsValid) {
                transferRepository.InsertOrUpdate(transfer);
                transferRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleInitiators = personRepository.All;
				ViewBag.PossibleAccounts = accountRepository.All;
				ViewBag.PossibleToAccounts = accountRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Transfers/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleInitiators = personRepository.All;
			ViewBag.PossibleAccounts = accountRepository.All;
			ViewBag.PossibleToAccounts = accountRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
             return View(transferRepository.Find(id));
        }

        //
        // POST: /Transfers/Edit/5

        [HttpPost]
        public ActionResult Edit(Transfer transfer)
        {
            if (ModelState.IsValid) {
                transferRepository.InsertOrUpdate(transfer);
                transferRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleInitiators = personRepository.All;
				ViewBag.PossibleAccounts = accountRepository.All;
				ViewBag.PossibleToAccounts = accountRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /Transfers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(transferRepository.Find(id));
        }

        //
        // POST: /Transfers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            transferRepository.Delete(id);
            transferRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                personRepository.Dispose();
                accountRepository.Dispose();
                categoryRepository.Dispose();
                transferRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

