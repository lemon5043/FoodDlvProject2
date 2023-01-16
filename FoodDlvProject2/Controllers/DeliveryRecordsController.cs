using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace FoodDlvProject2.Controllers
{
	public class DeliveryRecordsController : Controller
	{
		private readonly AppDbContext _context;

		public DeliveryRecordsController(AppDbContext context)
		{
			_context = context;
		}
		// GET: DeliveryRecords
		public async Task<IActionResult> Index()
		{
			var appDbContext = await _context.Orders
				.Join(_context.OrderSchedules.Where(x => x.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
				{
					o.Id,
					o.Milage,
					s.MarkTime,
					s.Status.Status,
					o.DeliveryDrivers.FirstName,
					o.DeliveryDrivers.LastName,
					o.DeliveryDriversId,
				}).ToListAsync();

			var result =appDbContext
				.GroupBy(r => r.Id)
				.Select(g => g.OrderByDescending(r => r.MarkTime).FirstOrDefault())
				.Select(s => new DeliveryRecordIndexVM { 
					Id = s.Id, 
					OrderDate = s.MarkTime, 
					Milage = s.Milage, 
					Status = s.Status.ToString(), 
					DriverName = s.LastName + s.FirstName, 
					DeliveryDriversId = s.DeliveryDriversId 
				});

			return View(result);
		}

		// GET: DeliveryRecords/MonthlyDetails/5
		public async Task<IActionResult> MonthlyDetails(int? id)
		{
			if (id == null || _context.Orders == null)
			{
				return NotFound();
			}

			var DeliveryRecords = await _context.Orders
				.Where(m => m.DeliveryDriversId == id)
				.Join(_context.OrderSchedules.Where(s => s.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
				{
					o.Id,
					o.Milage,
					s.MarkTime,
					o.DeliveryDrivers.FirstName,
					o.DeliveryDrivers.LastName,
				}).ToListAsync();

			var result = DeliveryRecords.GroupBy(r => r.Id)
			.Select(g => g.OrderByDescending(r => r.MarkTime)
			.FirstOrDefault())
			.Select(s => new
			{
				Id = s.Id,
				OrderDate = s.MarkTime,
				Milage = s.Milage,
				DriverName = s.LastName + s.FirstName,
			})
			.GroupBy(r => r.OrderDate.Month).ToList()
			.Select(s => new DeliveryMonthlyDetailRecordVM
			{
				TotalMilage = s.Sum(x => x.Milage),
				TotalDelievery = s.Count(),
				OrderDate = s.Min(x => x.OrderDate),
				DriverName = s.Select(x => x.DriverName).FirstOrDefault(),
			})
			;
			if (DeliveryRecords == null)
			{
				return NotFound();
			}
			ViewBag.DriverId = id;
			ViewBag.DriverName = result.Select(x => x.DriverName).FirstOrDefault();
			return View(result);
		}
		// GET: DeliveryRecords/IndividualMonthlyDetails/5
		public async Task<IActionResult> IndividualMonthlyDetails(int? year, int? month, int? id)
		{
			if (id == null || month == null || year == null || _context.Orders == null)
			{
				return NotFound();
			}

			var DeliveryRecords = await _context.Orders
				.Include(d => d.DeliveryDrivers)
				.Where(m => m.DeliveryDriversId == id)
				.Join(_context.OrderSchedules.Where(s => s.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
				{
					o.Id,
					o.Milage,
					s.MarkTime,
					s.Status.Status,
					o.DeliveryDrivers.FirstName,
					o.DeliveryDrivers.LastName,
				}).ToListAsync();

			var result = DeliveryRecords.GroupBy(r => r.Id)
			.Select(g => g.OrderByDescending(r => r.MarkTime)
			.FirstOrDefault())
			.Where(x => x.MarkTime.Year == year && x.MarkTime.Month == month)
			.Select(s => new DeliveryIndividualDetailsRecordVM
			{
				Id = s.Id,
				OrderDate = s.MarkTime,
				Milage = s.Milage,
				Status = s.Status.ToString(),
				DriverName = s.LastName + s.FirstName,
			});


			if (DeliveryRecords == null)
			{
				return NotFound();
			}
			ViewBag.Year = year;
			ViewBag.Month = month;
			ViewBag.DriverId = id;
			ViewBag.DriverName = result.Select(x => x.DriverName).FirstOrDefault();
			return View(result);
		}

	}
}
