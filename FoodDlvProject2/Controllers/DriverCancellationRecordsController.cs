using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FoodDlvProject2.Controllers
{
	public class DriverCancellationRecordsController : Controller
	{
		private readonly DeliveryCancellationRecordService deliveryCancellationRecordService;

		public DriverCancellationRecordsController()
		{
			var db = new AppDbContext();
			IDeliveryCancellationRecordRepository repository = new DeliveryCancellationRecordRepository(db);
			this.deliveryCancellationRecordService = new DeliveryCancellationRecordService(repository);
		}
		// GET: DeliveryDrivers
		public async Task<IActionResult> Index()
		{
			var data = await deliveryCancellationRecordService.GetCancellationRecordsAsync();
			return View(data.Select(x => x.ToDriverCancellationRecordsIndexVM()));
		}

		// GET: DeliveryDrivers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			try
			{
				var data = await deliveryCancellationRecordService.GetPersonalCancellationRecordsAsync(id);

				ViewBag.DriverId = id;
				ViewBag.DriverName = data.Select(x => x.DriverName).FirstOrDefault();

				return View(data.Select(x=>x.ToDeliveryViolationRecordsIndexVM()));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}
		// GET: DeliveryDrivers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			var data = await deliveryCancellationRecordService.GetEditAsync(id);
			var VM = data.ToDriverCancellationRecordsEditVM();

			ViewData["CancellationId"] = await deliveryCancellationRecordService.GetListAsync(VM.CancellationId);

			return View(VM);
		}

		// POST: DeliveryDrivers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CancellationId,CancellationDate")] DriverCancellationRecordsEditVM DriverCancellationRecords)
		{
			ModelState.Remove("DriverName");
			if (ModelState.IsValid)
			{
				try
				{
					TempData["Result"] = await deliveryCancellationRecordService.EditAsync(DriverCancellationRecords.ToDriverCancellationRecordDTO());
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
					ViewData["CancellationId"] = await deliveryCancellationRecordService.GetListAsync(DriverCancellationRecords.CancellationId);
					return View(await GetEditAsync(id));
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["CancellationId"] = await deliveryCancellationRecordService.GetListAsync(DriverCancellationRecords.CancellationId);
			return View(await GetEditAsync(id));
		}

		private async Task<DriverCancellationRecordsEditVM> GetEditAsync(int? id)
		{
			var data = await deliveryCancellationRecordService.GetEditAsync(id);
			var VM = data.ToDriverCancellationRecordsEditVM();
			return VM;
		}
	}
}
