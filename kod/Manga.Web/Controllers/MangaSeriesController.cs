using Manga.Model;
using Manga.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Manga.Web.Controllers
{
    public class MangaSeriesController : Controller
    {
        private readonly AppDbContext _context;

        public MangaSeriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MangaSeries.Include(b => b.Publisher);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> DetailsSeries(int? id)
        {
            if (id == null || _context.MangaSeries == null)
            {
                return NotFound();
            }

            var series = await _context.MangaSeries
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateSeries()
        {
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSeries([Bind("Id,Name,SeriesISBN,Completed,PublisherId")] MangaSeries mangaSeries)
        {
            ModelState["Publisher"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                _context.Add(mangaSeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", mangaSeries.PublisherId);
            return View(mangaSeries);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSeries(int? id)
        {
            if (id == null || _context.MangaVolumes == null)
            {
                return NotFound();
            }

            var series = await _context.MangaSeries.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", series.PublisherId);
            return View(series);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSeries(int id, [Bind("Id,Name,SeriesISBN,Completed,PublisherId")] MangaSeries mangaSeries)
        {
            if (id != mangaSeries.Id)
            {
                return NotFound();
            }
            ModelState["Publisher"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mangaSeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaSeriesExists(mangaSeries.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", mangaSeries.PublisherId);
            return View(mangaSeries);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSeries(int? id)
        {
            if (id == null || _context.MangaSeries == null)
            {
                return NotFound();
            }

            var series = await _context.MangaSeries
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        [HttpPost, ActionName("DeleteSeries")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MangaSeries == null)
            {
                return Problem("Entity set 'AppDbContext.MangaSeries'  is null.");
            }
            var series = await _context.MangaSeries.FindAsync(id);
            if (series != null)
            {
                _context.MangaSeries.Remove(series);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaSeriesExists(int id)
        {
            return (_context.MangaSeries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
