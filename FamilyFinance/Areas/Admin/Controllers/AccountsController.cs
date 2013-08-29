using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class AccountsController : Controller
    {
		private readonly IPersonRepository personRepository;
		private readonly IAccountTypeRepository accounttypeRepository;
		private readonly IAccountRepository accountRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountsController() : this(new PersonRepository(), new AccountTypeRepository(), new AccountRepository())
        {
        }

        public AccountsController(IPersonRepository personRepository, IAccountTypeRepository accounttypeRepository, IAccountRepository accountRepository)
        {
			this.personRepository = personRepository;
			this.accounttypeRepository = accounttypeRepository;
			this.accountRepository = accountRepository;
        }

        //
        // GET: /Accounts/

        public ViewResult Index()
        {
            return View(accountRepository.AllIncluding(account => account.Owner, account => account.AccountType));
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
			ViewBag.PossibleAccountTypes = accounttypeRepository.All;
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
				ViewBag.PossibleAccountTypes = accounttypeRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Accounts/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleOwners = personRepository.All;
			ViewBag.PossibleAccountTypes = accounttypeRepository.All;
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
				ViewBag.PossibleAccountTypes = accounttypeRepository.All;
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
                accounttypeRepository.Dispose();
                accountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

