using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.Models.EFModels;

namespace FoodDlvProject2.Controllers
{
    public class MemberViolationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public MemberViolationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MemberViolationTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.MemberViolationTypes.ToListAsync());
        }

        // GET: MemberViolationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberViolationTypes == null)
            {
                return NotFound();
            }

            var memberViolationType = await _context.MemberViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberViolationType == null)
            {
                return NotFound();
            }

            return View(memberViolationType);
        }

        // GET: MemberViolationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberViolationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ViolationContent,Content")] MemberViolationType memberViolationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberViolationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberViolationType);
        }

        // GET: MemberViolationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberViolationTypes == null)
            {
                return NotFound();
            }

            var memberViolationType = await _context.MemberViolationTypes.FindAsync(id);
            if (memberViolationType == null)
            {
                return NotFound();
            }
            return View(memberViolationType);
        }

        // POST: MemberViolationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ViolationContent,Content")] MemberViolationType memberViolationType)
        {
            if (id != memberViolationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberViolationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberViolationTypeExists(memberViolationType.Id))
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
            return View(memberViolationType);
        }

        // GET: MemberViolationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberViolationTypes == null)
            {
                return NotFound();
            }

            var memberViolationType = await _context.MemberViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberViolationType == null)
            {
                return NotFound();
            }

            return View(memberViolationType);
        }

        // POST: MemberViolationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberViolationTypes == null)
            {
                return Problem("Entity set 'AppDbContext.MemberViolationTypes'  is null.");
            }
            var memberViolationType = await _context.MemberViolationTypes.FindAsync(id);
            if (memberViolationType != null)
            {
                _context.MemberViolationTypes.Remove(memberViolationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberViolationTypeExists(int id)
        {
          return _context.MemberViolationTypes.Any(e => e.Id == id);
        }
    }
}
