using System.Web.Mvc;
using FamilyFinance.Areas.Admin.Models;
using FamilyFinance.Areas.Admin.Models.Finance;
using FamilyFinance.Models;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class AccountsController : Controller
    {
		private readonly IPersonRepository personRepository;
		private readonly IAccountRepository accountRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountsController() : this(new PersonRepository(), new AccountRepository())
        {
        }

        public AccountsController(IPersonRepository personRepository, IAccountRepository accountRepository)
        {
			this.personRepository = personRepository;
			this.accountRepository = accountRepository;
        }

        //
        // GET: /Accounts/

        public ViewResult Index()
        {
            return View(accountRepository.AllIncluding(account => account.Owner));
        }

        //
        // GET: /Accounts/Details/5

        public ViewResult Details(int id)
        {
            return View(accountRepository.Find(id));
        }

        //
        // GET: /Accounts/Create

        public ActionResult Create()
        {
			ViewBag.PossibleOwners = personRepository.All;
            return View();
        } 

        //
        // POST: /Accounts/Create

        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid) {
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOwners = personRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Accounts/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleOwners = personRepository.All;
             return View(accountRepository.Find(id));
        }

        //
        // POST: /Accounts/Edit/5

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid) {
                accountRepository.InsertOrUpdate(account);
                accountRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOwners = personRepository.All;
				return View();
			}
        }

        //
        // GET: /Accounts/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(accountRepository.Find(id));
        }

        //
        // POST: /Accounts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            accountRepository.Delete(id);
            accountRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                personRepository.Dispose();
                accountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

