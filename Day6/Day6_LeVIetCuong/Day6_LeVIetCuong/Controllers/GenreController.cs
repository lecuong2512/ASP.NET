using Day6_LeVIetCuong.Data;
using Day6_LeVIetCuong.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day6_LeVIetCuong.Controllers
{
    public class GenreController : Controller
    {
        private readonly Day6_LeVIetCuongContext _context;

        public GenreController(Day6_LeVIetCuongContext context)
        {
            _context = context;
        }

        // GET: Genre
        public async Task<IActionResult> Index(string? searchString)
        {
            var query = _context.Genres.Include(g => g.Movies).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var keyword = searchString.Trim();
                query = query.Where(g => g.Name.Contains(keyword));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await query.OrderBy(g => g.Id).ToListAsync());
        }

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        // GET: Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // GET: Genre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Genre genre)
        {
            if (id != genre.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Genres.AnyAsync(e => e.Id == genre.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // GET: Genre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (genre.Movies != null && genre.Movies.Any())
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Không thể xóa thể loại \"{genre.Name}\" vì đang có {genre.Movies.Count} phim thuộc thể loại này."
                );
                return View("Delete", genre);
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
