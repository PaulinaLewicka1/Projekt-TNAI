using Manga.Model;
using Manga.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Manga.Web.Controllers
{
    public class MangaVolumesController : Controller
    {
        private readonly AppDbContext _context;

        public MangaVolumesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MangaVolumes.Include(b => b.Series);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MangaVolumes == null)
            {
                return NotFound();
            }

            var volume = await _context.MangaVolumes
                .Include(b => b.Series)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MangaSeriesId"] = new SelectList(_context.MangaSeries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,ISBN,Title,Count,AuthorId")] MangaVolume mangaVolume)
        {
            ModelState["Series"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                _context.Add(mangaVolume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MangaSeriesId"] = new SelectList(_context.MangaSeries, "Id", "Name", mangaVolume.MangaSeriesId);
            return View(mangaVolume);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MangaVolumes == null)
            {
                return NotFound();
            }

            var volume = await _context.MangaVolumes.FindAsync(id);
            if (volume == null)
            {
                return NotFound();
            }
            ViewData["MangaSeriesId"] = new SelectList(_context.MangaSeries, "Id", "Name", volume.MangaSeriesId);
            return View(volume);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Name,Authors,Description,VolumeNumber,Year,MangaSeriesId")] MangaVolume mangaVolume)
        {
            if (id != mangaVolume.Id)
            {
                return NotFound();
            }
            ModelState["MangaSeries"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mangaVolume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaVolumeExists(mangaVolume.Id))
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
            ViewData["MangaSeriesId"] = new SelectList(_context.MangaSeries, "Id", "Name", mangaVolume.MangaSeriesId);
            return View(mangaVolume);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MangaVolumes == null)
            {
                return NotFound();
            }

            var volume = await _context.MangaVolumes
                .Include(b => b.Series)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MangaVolumes == null)
            {
                return Problem("Entity set 'AppDbContext.Books'  is null.");
            }
            var volume = await _context.MangaVolumes.FindAsync(id);
            if (volume != null)
            {
                _context.MangaVolumes.Remove(volume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaVolumeExists(int id)
        {
            return (_context.MangaVolumes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
