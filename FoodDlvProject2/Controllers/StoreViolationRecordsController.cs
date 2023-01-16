using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Controllers
{
    public class StoreViolationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public StoreViolationRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreViolationRecords
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StoreViolationRecords.Include(s => s.Order).Include(s => s.Store).Include(s => s.Violation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StoreViolationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreViolationRecords == null)
            {
                return NotFound();
            }

            var storeViolationRecord = await _context.StoreViolationRecords
                .Include(s => s.Order)
                .Include(s => s.Store)
                .Include(s => s.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeViolationRecord == null)
            {
                return NotFound();
            }

            return View(storeViolationRecord);
        }

        // GET: StoreViolationRecords/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName");
            ViewData["ViolationId"] = new SelectList(_context.StoreViolationTypes, "Id", "ViolationContent");
            return View();
        }

        // POST: StoreViolationRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoreId,ViolationId,OrderId,ViolationDate")] StoreViolationRecord storeViolationRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeViolationRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeViolationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeViolationRecord.StoreId);
            ViewData["ViolationId"] = new SelectList(_context.StoreViolationTypes, "Id", "ViolationContent", storeViolationRecord.ViolationId);
            return View(storeViolationRecord);
        }

        // GET: StoreViolationRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreViolationRecords == null)
            {
                return NotFound();
            }

            var storeViolationRecord = await _context.StoreViolationRecords.FindAsync(id);
            if (storeViolationRecord == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeViolationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeViolationRecord.StoreId);
            ViewData["ViolationId"] = new SelectList(_context.StoreViolationTypes, "Id", "ViolationContent", storeViolationRecord.ViolationId);
            return View(storeViolationRecord);
        }

        // POST: StoreViolationRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoreId,ViolationId,OrderId,ViolationDate")] StoreViolationRecord storeViolationRecord)
        {
            if (id != storeViolationRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeViolationRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreViolationRecordExists(storeViolationRecord.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeViolationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeViolationRecord.StoreId);
            ViewData["ViolationId"] = new SelectList(_context.StoreViolationTypes, "Id", "ViolationContent", storeViolationRecord.ViolationId);
            return View(storeViolationRecord);
        }

        // GET: StoreViolationRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreViolationRecords == null)
            {
                return NotFound();
            }

            var storeViolationRecord = await _context.StoreViolationRecords
                .Include(s => s.Order)
                .Include(s => s.Store)
                .Include(s => s.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeViolationRecord == null)
            {
                return NotFound();
            }

            return View(storeViolationRecord);
        }

        // POST: StoreViolationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreViolationRecords == null)
            {
                return Problem("Entity set 'AppDbContext.StoreViolationRecords'  is null.");
            }
            var storeViolationRecord = await _context.StoreViolationRecords.FindAsync(id);
            if (storeViolationRecord != null)
            {
                _context.StoreViolationRecords.Remove(storeViolationRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreViolationRecordExists(int id)
        {
          return _context.StoreViolationRecords.Any(e => e.Id == id);
        }
    }
}
