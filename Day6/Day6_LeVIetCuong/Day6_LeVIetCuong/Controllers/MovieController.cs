using Day6_LeVIetCuong.Data;
using Day6_LeVIetCuong.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day6_LeVIetCuong.Controllers
{
    public class MovieController : Controller
    {
        private readonly Day6_LeVIetCuongContext _context;

        public MovieController(Day6_LeVIetCuongContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index(string? searchString, int? genreId)
        {
            var moviesQuery = _context.Movies.Include(m => m.Genre).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var keyword = searchString.Trim();
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(keyword));
            }

            if (genreId.HasValue && genreId.Value > 0)
            {
                moviesQuery = moviesQuery.Where(m => m.GenreId == genreId.Value);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentGenreId"] = genreId;
            ViewData["GenreFilter"] = new SelectList(
                await _context.Genres.OrderBy(g => g.Id).ToListAsync(),
                "Id",
                "Name",
                genreId);

            return View(await moviesQuery.OrderBy(m => m.Id).ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // GET: Movie/Create
        public async Task<IActionResult> Create()
        {
            await PopulateGenresAsync();
            return View(new Movie { ReleaseDate = DateTime.Today });
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Duration,GenreId")] Movie movie)
        {
            if (!await _context.Genres.AnyAsync(g => g.Id == movie.GenreId))
            {
                ModelState.AddModelError(nameof(Movie.GenreId), "Thể loại đã chọn không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateGenresAsync(movie.GenreId);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            await PopulateGenresAsync(movie.GenreId);
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Duration,GenreId")] Movie movie)
        {
            if (id != movie.Id) return NotFound();

            if (!await _context.Genres.AnyAsync(g => g.Id == movie.GenreId))
            {
                ModelState.AddModelError(nameof(Movie.GenreId), "Thể loại đã chọn không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Movies.AnyAsync(e => e.Id == movie.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateGenresAsync(movie.GenreId);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateGenresAsync(int? selectedGenreId = null)
        {
            ViewData["GenreId"] = new SelectList(
                await _context.Genres.OrderBy(g => g.Id).ToListAsync(),
                "Id",
                "Name",
                selectedGenreId);
        }
    }
}
