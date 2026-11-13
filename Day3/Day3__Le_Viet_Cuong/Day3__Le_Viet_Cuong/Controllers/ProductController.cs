using Day3__Le_Viet_Cuong.Data;
using Day3__Le_Viet_Cuong.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day3__Le_Viet_Cuong.Controllers
{
    public class ProductController : Controller
    {
        private const int PageSize = 5;

        public IActionResult Index(int page = 1)
        {
            var products = SeedData.products.OrderBy(p => p.Id).ToList();
            var totalPages = (int)Math.Ceiling(products.Count / (double)PageSize);
            var pageNumber = totalPages == 0 ? 1 : Math.Clamp(page, 1, totalPages);

            var viewModel = new ProductListViewModel
            {
                Products = products.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList(),
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View(new ProductViewModel
            {
                Categories = GetCategories()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel viewModel)
        {
            var product = viewModel.Product;
            var category = SeedData.categories.FirstOrDefault(c => c.Id == product.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError("Product.CategoryId", "Please select a valid category");
            }

            if (ModelState.IsValid)
            {
                product.Id = SeedData.products.Count == 0 ? 1 : SeedData.products.Max(p => p.Id) + 1;
                product.Category = category;
                SeedData.products.Add(product);
                category!.Products.Add(product);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = GetCategories(product.CategoryId);
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            return View(new ProductViewModel
            {
                Product = product,
                Categories = GetCategories(product.CategoryId)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel viewModel)
        {
            var product = viewModel.Product;
            var existingProduct = SeedData.products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct is null)
            {
                return NotFound();
            }

            var category = SeedData.categories.FirstOrDefault(c => c.Id == product.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError("Product.CategoryId", "Please select a valid category");
            }

            if (ModelState.IsValid)
            {
                existingProduct.Category?.Products.Remove(existingProduct);
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Category = category;
                category!.Products.Add(existingProduct);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = GetCategories(product.CategoryId);
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            var existingProduct = SeedData.products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct is null)
            {
                return NotFound();
            }

            existingProduct.Category?.Products.Remove(existingProduct);
            SeedData.products.Remove(existingProduct);
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetCategories(int? selectedCategoryId = null)
        {
            return new SelectList(SeedData.categories, "Id", "Name", selectedCategoryId);
        }
    }
}
