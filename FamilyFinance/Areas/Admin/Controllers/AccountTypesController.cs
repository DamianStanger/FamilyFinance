using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class AccountTypesController : Controller
    {
		private readonly IAccountTypeRepository accountTypeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountTypesController() : this(new AccountTypeRepository())
        {
        }

        public AccountTypesController(IAccountTypeRepository accountTypeRepository)
        {
			this.accountTypeRepository = accountTypeRepository;
        }

        //
        // GET: /AccountType/

        public ViewResult Index()
        {
            return View(accountTypeRepository.All);
        }

        //
        // GET: /AccountType/Details/5

        public ViewResult Details(int id)
        {
            return View(accountTypeRepository.Find(id));
        }

        //
        // GET: /AccountType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AccountType/Create

        [HttpPost]
        public ActionResult Create(AccountType accounttype)
        {
            if (ModelState.IsValid) {
                accountTypeRepository.InsertOrUpdate(accounttype);
                accountTypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /AccountType/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(accountTypeRepository.Find(id));
        }

        //
        // POST: /AccountType/Edit/5

        [HttpPost]
        public ActionResult Edit(AccountType accounttype)
        {
            if (ModelState.IsValid) {
                accountTypeRepository.InsertOrUpdate(accounttype);
                accountTypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /AccountType/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(accountTypeRepository.Find(id));
        }

        //
        // POST: /AccountType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            accountTypeRepository.Delete(id);
            accountTypeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                accountTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

