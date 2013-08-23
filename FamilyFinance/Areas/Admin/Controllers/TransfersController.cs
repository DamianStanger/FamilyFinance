using System.Web.Mvc;
using FamilyFinance.Areas.Admin.Models;
using FamilyFinance.Areas.Admin.Models.Finance;
using FamilyFinance.Models;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class TransfersController : Controller
    {
		private readonly IAccountRepository accountRepository;
		private readonly ITransferRepository transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TransfersController() : this(new AccountRepository(), new TransferRepository())
        {
        }

        public TransfersController(IAccountRepository accountRepository, ITransferRepository transferRepository)
        {
			this.accountRepository = accountRepository;
			this.transferRepository = transferRepository;
        }

        //
        // GET: /Transfers/

        public ViewResult Index()
        {
            return View(transferRepository.AllIncluding(transfer => transfer.FromAccount, transfer => transfer.ToAccount));
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
			ViewBag.PossibleFromAccounts = accountRepository.All;
			ViewBag.PossibleToAccounts = accountRepository.All;
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
				ViewBag.PossibleFromAccounts = accountRepository.All;
				ViewBag.PossibleToAccounts = accountRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Transfers/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleFromAccounts = accountRepository.All;
			ViewBag.PossibleToAccounts = accountRepository.All;
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
				ViewBag.PossibleFromAccounts = accountRepository.All;
				ViewBag.PossibleToAccounts = accountRepository.All;
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
                accountRepository.Dispose();
                transferRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

