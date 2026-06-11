using FurnitureFactory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var materials = _context.Materials.Include(m => m.Category);
            return View(await materials.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materials
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null) return NotFound();
            return View(material);
        }

        public IActionResult Create()
        {
            ViewData["MaterialCategoryId"] = new SelectList(_context.MaterialCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArticleNumber,UnitOfMeasure,MinStockLevel,CurrentStock,Description,MaterialCategoryId")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialCategoryId"] = new SelectList(_context.MaterialCategories, "Id", "Name", material.MaterialCategoryId);
            return View(material);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();
            ViewData["MaterialCategoryId"] = new SelectList(_context.MaterialCategories, "Id", "Name", material.MaterialCategoryId);
            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ArticleNumber,UnitOfMeasure,MinStockLevel,CurrentStock,Description,MaterialCategoryId")] Material material)
        {
            if (id != material.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Materials.Any(e => e.Id == material.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialCategoryId"] = new SelectList(_context.MaterialCategories, "Id", "Name", material.MaterialCategoryId);
            return View(material);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var material = await _context.Materials
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null) return NotFound();
            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}