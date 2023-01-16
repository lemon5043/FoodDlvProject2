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
    public class ComplaintTypesController : Controller
    {
        private readonly AppDbContext _context;

        public ComplaintTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ComplaintTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ComplaintTypes.ToListAsync());
        }

        // GET: ComplaintTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComplaintTypes == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }

            return View(complaintType);
        }

        // GET: ComplaintTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComplaintTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComplaintType1")] ComplaintType complaintType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintType);
        }

        // GET: ComplaintTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComplaintTypes == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintTypes.FindAsync(id);
            if (complaintType == null)
            {
                return NotFound();
            }
            return View(complaintType);
        }

        // POST: ComplaintTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComplaintType1")] ComplaintType complaintType)
        {
            if (id != complaintType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintTypeExists(complaintType.Id))
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
            return View(complaintType);
        }

        // GET: ComplaintTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComplaintTypes == null)
            {
                return NotFound();
            }

            var complaintType = await _context.ComplaintTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }

            return View(complaintType);
        }

        // POST: ComplaintTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComplaintTypes == null)
            {
                return Problem("Entity set 'AppDbContext.ComplaintTypes'  is null.");
            }
            var complaintType = await _context.ComplaintTypes.FindAsync(id);
            if (complaintType != null)
            {
                _context.ComplaintTypes.Remove(complaintType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintTypeExists(int id)
        {
          return _context.ComplaintTypes.Any(e => e.Id == id);
        }
    }
}
