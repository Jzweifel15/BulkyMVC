using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        // Used for accessing Model objects from the DB
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
            // Simply navigate to the create category view
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            // server-side validation
            if (newCategory.Name == newCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            // server-side validation
            if (ModelState.IsValid)
            {
                // Actually add the category
                _db.Categories.Add(newCategory);
                _db.SaveChanges();

                // Redirect back to the Index view
                return RedirectToAction("Index");
            }

            // Do nothing if the form is incomplete or there were errors
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            // server-side validation
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            // Find the category to edit by the Id
            Category? category = _db.Categories.Find(id);

            /** Other ways to find the model
             *  
             *  Category? category = _db.Categories.FirstOrDefault(cat => cat.Id == id);
             *  Category? category = _db.Categories.Where(cat => cat.Id == id).FirstOrDefault();
             *  
             */

            // server-side validation
            if (category == null)
            {
                return NotFound();
            }

            // Redirect to the Edit view with the form pre-populated with the found Category data
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {
            // server-side validation
            if (ModelState.IsValid)
            {
                // Actually update the Category's data
                _db.Categories.Update(updatedCategory);
                _db.SaveChanges();

                // Redirect back to the Index view
                return RedirectToAction("Index");
            }

            // Do nothing if the form is incomplete or there were errors
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id) 
        { 
            // server-side validation
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Find the category to be deleted by its Id
            Category? category = _db.Categories.Find(id);

            // server-side validation
            if (category == null)
            {
                return NotFound();
            }

            // This will not return a view. It will simply delete the found category (if no errors)
            // and stay on the Index view
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            // Find the category to be deleted by its Id
            Category? category = _db.Categories.Find(id);

            // server-side validation
            if (category == null)
            {
                return NotFound();
            }

            // Actually delete the category
            _db.Categories.Remove(category);
            _db.SaveChanges();

            // We did not actually go to a different view, so stay on the Index view after deletion
            return RedirectToAction("Index");
        }
    }
}
