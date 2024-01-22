using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
      
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Retrieves all Category records from the Db
            List<Category> categoryList = _db.Categories.ToList();
         
            // Pass List of all categories to the Index view
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (newCategory.Name == newCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(newCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            Category? category = _db.Categories.Find(id);

            /** Other ways to find the model
             *  
             *  Category? category = _db.Categories.FirstOrDefault(cat => cat.Id == id);
             *  Category? category = _db.Categories.Where(cat => cat.Id == id).FirstOrDefault();
             *  
             */

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(updatedCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
