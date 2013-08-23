using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Finance;
using FamilyFinance.Models;

namespace FamilyFinance.Controllers
{   
    public class AccountsController : Controller
    {
		private readonly IAccountRepository accountRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountsController() : this(new AccountRepository())
        {
        }

        public AccountsController(IAccountRepository accountRepository)
        {
			this.accountRepository = accountRepository;
        }

        //
        // GET: /Accounts/

        public ViewResult Index()
        {
            return View(accountRepository.All);
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
				return View();
			}
        }
        
        //
        // GET: /Accounts/Edit/5
 
        public ActionResult Edit(int id)
        {
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
                accountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

