
using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelClasses;

namespace MiahWholeSale.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.categories.ToList();
            //if (!items.Any())
            //{
            //    Console.WriteLine("No categories found in the database.");
            //}
            return View(items);
        }
        public IActionResult Upsert(int? id)
        {
            
            if (id == 0)
            {
                Category category = new Category();
                return View(category);
            }
            else
            {
                var items = _context.categories.FirstOrDefault(c => c.Id ==id);
                return View(items);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, Category category)
        {
            if (id == null)
            {
                var foundItem = await _context.categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (foundItem != null)
                {
                    return RedirectToAction("Index");
                }

                await _context.categories.AddAsync(category); 
            }
            else
            {
                var items = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
                if (items != null)
                {
                    items.Name = category.Name;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
