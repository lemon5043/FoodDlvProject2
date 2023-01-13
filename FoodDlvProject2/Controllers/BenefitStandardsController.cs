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
    public class BenefitStandardsController : Controller
    {
        private readonly AppDbContext _context;

        public BenefitStandardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BenefitStandards
        public async Task<IActionResult> Index()
        {
              return View(await _context.BenefitStandards.ToListAsync());
        }

        // GET: BenefitStandards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BenefitStandards == null)
            {
                return NotFound();
            }

            var benefitStandard = await _context.BenefitStandards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benefitStandard == null)
            {
                return NotFound();
            }

            return View(benefitStandard);
        }

        // GET: BenefitStandards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BenefitStandards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerOrder,PerMilage,BonusThreshold1,BonusThreshold2,BonusThreshold3,Bouns1,Bouns2,Bouns3,HolidayBouns,RushHoursBouns,RushHoursStart1,RushHoursStart2,RushHoursEnd1,RushHoursEnd2,Selected")] BenefitStandard benefitStandard)
        {
            if (ModelState.IsValid)
            {
                if (benefitStandard.Selected)
                {
                    _context.Update(BenefitStandardSSelected());
                    await _context.SaveChangesAsync();
                }
                _context.Add(benefitStandard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benefitStandard);
        }

        // GET: BenefitStandards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BenefitStandards == null)
            {
                return NotFound();
            }

            var benefitStandard = await _context.BenefitStandards.FindAsync(id);
            if (benefitStandard == null)
            {
                return NotFound();
            }
            return View(benefitStandard);
        }

        // POST: BenefitStandards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,PerOrder,PerMilage,BonusThreshold1,BonusThreshold2,BonusThreshold3,Bouns1,Bouns2,Bouns3,HolidayBouns,RushHoursBouns,RushHoursStart1,RushHoursStart2,RushHoursEnd1,RushHoursEnd2,Selected")] BenefitStandard benefitStandard)
        {
            if (id != benefitStandard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if( benefitStandard.Selected == false && benefitStandard.Id == _context.BenefitStandards.FirstOrDefault(e => e.Selected == true).Id)
                    {
                        return Problem("正在使用的方案不可取消");
					}
                    if (benefitStandard.Selected)
                    {
                        List<BenefitStandard> list = new List<BenefitStandard> {benefitStandard, BenefitStandardSSelected()};
                        foreach (var item in list)
                        {
                            var existing = _context.BenefitStandards.Find(item.Id);
                            if (existing != null)
                            {
                                _context.Entry(existing).CurrentValues.SetValues(item);
                            }
                        }
                    }
                    else
                    {
                        _context.Update(benefitStandard);
                    }
                    
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenefitStandardExists(benefitStandard.Id))
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
            return View(benefitStandard);
        }

        // GET: BenefitStandards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BenefitStandards == null)
            {
                return NotFound();
            }
            var benefitStandard = await _context.BenefitStandards
                .FirstOrDefaultAsync(m => m.Id == id);
			if (benefitStandard.Selected == true) return Problem("正在使用的方案不可刪除");
			if (benefitStandard == null)
            {
                return NotFound();
            }

            return View(benefitStandard);
        }

        // POST: BenefitStandards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BenefitStandards == null)
            {
                return Problem("Entity set 'AppDbContext.BenefitStandards'  is null.");
            }
            var benefitStandard = await _context.BenefitStandards.FindAsync(id);
           
			if (benefitStandard != null)
            {
                _context.BenefitStandards.Remove(benefitStandard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenefitStandardExists(int id)
        {
          return _context.BenefitStandards.Any(e => e.Id == id);
        }
		private BenefitStandard? BenefitStandardSSelected()
		{
            int? id = _context.BenefitStandards.FirstOrDefault(e => e.Selected == true).Id;
            if (id == null) return null;
			var selectItem =  _context.BenefitStandards.Find(id);
			selectItem.Selected = false;
			return selectItem;
		}
	}
}
