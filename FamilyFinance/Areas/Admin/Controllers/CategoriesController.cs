using System.Web.Mvc;
using FamilyFinance.Models;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;

namespace FamilyFinance.Areas.Admin.Controllers
{   
    public class CategoriesController : Controller
    {
		private readonly ICategoryRepository categoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CategoriesController() : this(new CategoryRepository())
        {
        }

        public CategoriesController(ICategoryRepository categoryRepository)
        {
			this.categoryRepository = categoryRepository;
        }

        //
        // GET: /Categories/

        public ViewResult Index()
        {
            return View(categoryRepository.AllIncluding(category => category.ParentCategory));
        }

        //
        // GET: /Categories/Details/5

        public ViewResult Details(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
			ViewBag.PossibleParentCategories = categoryRepository.All;
            return View();
        } 

        //
        // POST: /Categories/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleParentCategories = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Categories/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleParentCategories = categoryRepository.All;
             return View(categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleParentCategories = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /Categories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryRepository.Delete(id);
            categoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

