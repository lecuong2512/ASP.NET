using Day3__Le_Viet_Cuong.Data;
using Day3__Le_Viet_Cuong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day3__Le_Viet_Cuong.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index()
        {
            var categories = Data.SeedData.categories;
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            // lay ra category co id truyen vao tu seeddata
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Category category)
        {
            if (ModelState.IsValid)
            {
                //lay max id trong seeddata
                int maxId = SeedData.categories.Max(c => c.Id);
                // set id cho category moi tao
                category.Id = maxId + 1;
                // add category vao seeddata
                SeedData.categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Category category)
        {
            var existingCategory = SeedData.categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Models.Category category)
        {
            var existingCategory = SeedData.categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            SeedData.categories.Remove(existingCategory);
            return RedirectToAction(nameof(Index));
        }
    }
}
