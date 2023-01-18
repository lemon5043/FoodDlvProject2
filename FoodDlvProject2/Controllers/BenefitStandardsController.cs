using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace FoodDlvProject2.Controllers
{
    public class BenefitStandardsController : Controller
    {
		private readonly BenefitStandardService benefitStandardService;

		public BenefitStandardsController()
		{
			var db = new AppDbContext();
			IBenefitStandardsRepository repository = new BenefitStandardsRepository(db);
			this.benefitStandardService = new BenefitStandardService(repository);
		}

		// GET: BenefitStandards
		public async Task<IActionResult> Index()
        {
            var data = await benefitStandardService.GetBenefitStandardsAsync();
            var VM = data.Select(x => x.ToBenefitStandardsIndexVM());
            return View(VM);
        }

        // GET: BenefitStandards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var data = await benefitStandardService.GetOneAsync(id);
                return View(data.ToBenefitStandardsDetailsVM());
            }
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;			
			}
            return RedirectToAction(nameof(Index));
		}

        // GET: BenefitStandards/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BenefitStandards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerOrder,PerMilage,BonusThreshold1,BonusThreshold2," +
            "BonusThreshold3,Bouns1,Bouns2,Bouns3,HolidayBouns,RushHoursBouns,RushHoursStart1,RushHoursStart2," +
            "RushHoursEnd1,RushHoursEnd2,Selected")] BenefitStandardCreateVM benefitStandardVM)
        {
            if (ModelState.IsValid)
            {
				try
				{
					TempData["Result"] = await benefitStandardService.CreateAsync(benefitStandardVM.ToBenefitStandardsDTO());
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
					return View(benefitStandardVM);
				}
				return RedirectToAction(nameof(Index)); 
            }
            return View(benefitStandardVM);
        }

        [Authorize(Roles = "Administrator,PowerUser")]
        // GET: BenefitStandards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
			try
			{
				var data = await benefitStandardService.GetOneAsync(id);
				return View(data.ToBenefitStandardsEditVM());
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: BenefitStandards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PerOrder,PerMilage,BonusThreshold1,BonusThreshold2," +
            "BonusThreshold3,Bouns1,Bouns2,Bouns3,Selected")] BenefitStandardEditVM benefitStandardVM)
        {
            if (ModelState.IsValid)
            {
				try
				{
					TempData["Result"] = await benefitStandardService.EditAsync(benefitStandardVM.ToBenefitStandardsDTO());
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
					return View(benefitStandardVM);
				}
				return RedirectToAction(nameof(Index));
            }
            return View(benefitStandardVM);
        }

        // GET: BenefitStandards/Delete/5
        [Authorize(Roles = "Administrator,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
			try
			{
				var data = await benefitStandardService.GetOneAsync(id);

                if (data.Selected) throw new Exception("正在使用的方案不可刪除");
				
                return View(data.ToBenefitStandardsDeleteVM());
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: BenefitStandards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["Result"] = await benefitStandardService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
	}
}
