using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;

namespace FoodDlvProject2.Controllers
{
	public class PaysController : Controller
	{
		private readonly PaysService paysService;

		public PaysController()
		{
			var db = new AppDbContext();
			IPaysRepository repository = new PaysRepository(db);
			this.paysService = new PaysService(repository);
		}

		// GET: Pays
		public async Task<IActionResult> Index()
		{
			var data = await paysService.GetPaysAsync();
			return View(data.Select(x=>x.ToPaysIndexVM()));
		}

        // GET: Pays/IndividualMonthly/5
        public async Task<IActionResult> MonthlyDetails(int? id)
        {
			var data = await paysService.GetMonthlyDetailsAsync(id);
			var VM = data.Select(x => x.ToPaysMonthlyDetailsVM());

			ViewBag.DriverId = id;
			ViewBag.DriverName = VM.Select(x=>x.DriversName).FirstOrDefault();

			return View(VM);
        }

        // GET: Pays/IndividualMonthly/5
        public async Task<IActionResult> IndividualMonthlyDetails(int? year, int? month, int? id)
		{
			var data = await paysService.GetIndividualMonthlyDetailsAsync(year, month, id);				
			return View(data.ToPaysIndividualMonthlyDetailsVM());
		}

		//// GET: Pays/Create
		//public IActionResult Create()
		//{
		//	ViewData["DeliveryDriversId"] = new SelectList(_context.DeliveryDrivers, "Id", "Account");
		//	return View();
		//}

		//// POST: Pays/Create
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("Id,DeliveryDriversId,DeliveryCount,TotalMilage,ShareProfit,TotalPay,SettlementMonth")] Pay pay)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		_context.Add(pay);
		//		await _context.SaveChangesAsync();
		//		return RedirectToAction(nameof(Index));
		//	}
		//	ViewData["DeliveryDriversId"] = new SelectList(_context.DeliveryDrivers, "Id", "Account", pay.DeliveryDriversId);
		//	return View(pay);
		//}

		//// GET: Pays/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//	if (id == null || _context.Pays == null)
		//	{
		//		return NotFound();
		//	}

		//	var pay = await _context.Pays.FindAsync(id);
		//	if (pay == null)
		//	{
		//		return NotFound();
		//	}
		//	ViewData["DeliveryDriversId"] = new SelectList(_context.DeliveryDrivers, "Id", "Account", pay.DeliveryDriversId);
		//	return View(pay);
		//}

		//// POST: Pays/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Id,DeliveryDriversId,DeliveryCount,TotalMilage,ShareProfit,TotalPay,SettlementMonth")] Pay pay)
		//{
		//	if (id != pay.Id)
		//	{
		//		return NotFound();
		//	}

		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_context.Update(pay);
		//			await _context.SaveChangesAsync();
		//		}
		//		catch (DbUpdateConcurrencyException)
		//		{
		//			if (!PayExists(pay.Id))
		//			{
		//				return NotFound();
		//			}
		//			else
		//			{
		//				throw;
		//			}
		//		}
		//		return RedirectToAction(nameof(Index));
		//	}
		//	ViewData["DeliveryDriversId"] = new SelectList(_context.DeliveryDrivers, "Id", "Account", pay.DeliveryDriversId);
		//	return View(pay);
		//}

		//// GET: Pays/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//	if (id == null || _context.Pays == null)
		//	{
		//		return NotFound();
		//	}

		//	var pay = await _context.Pays
		//		.Include(p => p.DeliveryDrivers)
		//		.FirstOrDefaultAsync(m => m.Id == id);
		//	if (pay == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(pay);
		//}

		//// POST: Pays/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	if (_context.Pays == null)
		//	{
		//		return Problem("Entity set 'AppDbContext.Pays'  is null.");
		//	}
		//	var pay = await _context.Pays.FindAsync(id);
		//	if (pay != null)
		//	{
		//		_context.Pays.Remove(pay);
		//	}

		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}

		//private bool PayExists(int id)
		//{
		//	return _context.Pays.Any(e => e.Id == id);
		//}
	}
}
