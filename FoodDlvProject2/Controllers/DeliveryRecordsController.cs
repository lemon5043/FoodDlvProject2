using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace FoodDlvProject2.Controllers
{
	public class DeliveryRecordsController : Controller
	{
        private readonly DeliveryRecordService deliveryRecordService;

        public DeliveryRecordsController()
        {
            var db = new AppDbContext();
            IDeliveryRecordsRepository repository = new DeliveryRecordsRepository(db);
            this.deliveryRecordService = new DeliveryRecordService(repository);
        }

        // GET: DeliveryRecords
        public async Task<IActionResult> Index()
		{
			var data = await deliveryRecordService.GetAllRecordAsync();

			return View(data.Select(x=>x.ToDeliveryRecordIndexVM()));
		}

		// GET: DeliveryRecords/MonthlyDetails/5
		public async Task<IActionResult> MonthlyDetails(int? id)
		{
			try
			{
				var data = await deliveryRecordService.GetMonthlyRecordAsync(id);
				ViewBag.DriverId = id;
				ViewBag.DriverName = data.Select(x => x.DriverName).FirstOrDefault();

				return View(data.Select(x=>x.ToDeliveryMonthlyDetailRecordVM()));
			}
			catch (Exception ex) 
			{
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }		           
		}

		// GET: DeliveryRecords/IndividualMonthlyDetails/5
		public async Task<IActionResult> IndividualMonthlyDetails(int? year, int? month, int? id)
		{
            try
            {
                var data = await deliveryRecordService.GetIndividualMonthlyRecordAsync(year, month, id);

                ViewBag.Year = year;
                ViewBag.Month = month;
                ViewBag.DriverId = id;
                ViewBag.DriverName = data.Select(x => x.DriverName).FirstOrDefault();

                return View(data.Select(x => x.ToDeliveryIndividualDetailsRecordVM()));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
		}
	}
}
