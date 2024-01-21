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

      public IActionResult Index()
      {
         // Retrieves all Category records from the Db
         List<Category> objCategoryList = _db.Categories.ToList();
         
         // Pass List of all categories to the Index view
         return View(objCategoryList);
      }

      public IActionResult Create()
      {
            return View();
      }
   }
}
