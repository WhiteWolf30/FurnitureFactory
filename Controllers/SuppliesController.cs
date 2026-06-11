using FurnitureFactory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Controllers
{
    public class SuppliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var supplies = _context.Supplies
                .Include(s => s.Supplier)
                .Include(s => s.ResponsibleEmployee)
                .Include(s => s.Contract);
            return View(await supplies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var supply = await _context.Supplies
                .Include(s => s.Supplier)
                .Include(s => s.ResponsibleEmployee)
                .Include(s => s.Contract)
                .Include(s => s.SupplyItems)
                    .ThenInclude(si => si.Material)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supply == null) return NotFound();
            return View(supply);
        }

        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(s => s.IsActive), "Id", "CompanyName");
            ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees.Where(e => e.IsActive), "Id", "FullName");
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "ContractNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceNumber,SupplierId,ContractId,SupplyDate,ExpectedDate,Status,TotalAmount,ResponsibleEmployeeId,Notes")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(s => s.IsActive), "Id", "CompanyName", supply.SupplierId);
            ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees.Where(e => e.IsActive), "Id", "FullName", supply.ResponsibleEmployeeId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "ContractNumber", supply.ContractId);
            return View(supply);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var supply = await _context.Supplies.FindAsync(id);
            if (supply == null) return NotFound();
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(s => s.IsActive), "Id", "CompanyName", supply.SupplierId);
            ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees.Where(e => e.IsActive), "Id", "FullName", supply.ResponsibleEmployeeId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "ContractNumber", supply.ContractId);
            return View(supply);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNumber,SupplierId,ContractId,SupplyDate,ExpectedDate,Status,TotalAmount,ResponsibleEmployeeId,Notes")] Supply supply)
        {
            if (id != supply.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Supplies.Any(e => e.Id == supply.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(s => s.IsActive), "Id", "CompanyName", supply.SupplierId);
            ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees.Where(e => e.IsActive), "Id", "FullName", supply.ResponsibleEmployeeId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "ContractNumber", supply.ContractId);
            return View(supply);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var supply = await _context.Supplies
                .Include(s => s.Supplier)
                .Include(s => s.ResponsibleEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null) return NotFound();
            return View(supply);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supply = await _context.Supplies.FindAsync(id);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}