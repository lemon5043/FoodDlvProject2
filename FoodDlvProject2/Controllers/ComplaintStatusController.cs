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
    public class ComplaintStatusController : Controller
    {
        private readonly AppDbContext _context;

        public ComplaintStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ComplaintStatus
        public async Task<IActionResult> Index()
        {
              return View(await _context.ComplaintStatuses.ToListAsync());
        }

        // GET: ComplaintStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComplaintStatuses == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintStatus == null)
            {
                return NotFound();
            }

            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComplaintStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status")] ComplaintStatus complaintStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComplaintStatuses == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses.FindAsync(id);
            if (complaintStatus == null)
            {
                return NotFound();
            }
            return View(complaintStatus);
        }

        // POST: ComplaintStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] ComplaintStatus complaintStatus)
        {
            if (id != complaintStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintStatusExists(complaintStatus.Id))
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
            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComplaintStatuses == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintStatus == null)
            {
                return NotFound();
            }

            return View(complaintStatus);
        }

        // POST: ComplaintStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComplaintStatuses == null)
            {
                return Problem("Entity set 'AppDbContext.ComplaintStatuses'  is null.");
            }
            var complaintStatus = await _context.ComplaintStatuses.FindAsync(id);
            if (complaintStatus != null)
            {
                _context.ComplaintStatuses.Remove(complaintStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintStatusExists(int id)
        {
          return _context.ComplaintStatuses.Any(e => e.Id == id);
        }
    }
}
