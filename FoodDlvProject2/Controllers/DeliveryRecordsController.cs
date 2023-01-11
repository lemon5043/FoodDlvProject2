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
		// GET: DeliveryDrivers
		public async Task<IActionResult> Index()
		{
			var appDbContext = _context.Orders.Include(d => d.DeliveryDrivers);
			return View(await appDbContext.ToListAsync());
		}

		// GET: DeliveryDrivers/Details/5
		public async Task<IActionResult> MonthlyDetails(int? id)
		{
			if (id == null || _context.Orders == null)
			{
				return NotFound();
			}

			var DeliveryRecords = await _context.Orders
				.Include(d => d.DeliveryDrivers)
				.Where(m => m.DeliveryDriversId == id)
				.Join(_context.OrderSchedules.Where(s=>s.StatusId>3), o => o.Id, s => s.OrderId, (o, s) => new
				{
					o.Id,o.DeliveryDriversId,o.Milage,s.MarkTime,s.OrderId,
				}).ToListAsync();

                IEnumerable<DeliveryRecordVM> a = DeliveryRecords.GroupBy(r=>r.OrderId)
				.Select(g => g.OrderByDescending(r => r.MarkTime)
				.FirstOrDefault())
				.Select(s => new 
				{
                    //DeliveryDriversId=s.DeliveryDriversId,
                    Id = s.Id,
					OrderDate = s.MarkTime,
					Milage = s.Milage,
				})
				.GroupBy(r => r.OrderDate.Month).ToList()
				.Select(s => new DeliveryRecordVM
                {
                    TotalMilage = s.Sum(x=>x.Milage),
					TotalDelievery = s.Count(),
					Month =s.Min(x=>x.OrderDate).Month,
				})
				;
			if (DeliveryRecords == null)
			{
				return NotFound();
			}
			ViewBag.DriverId = id;

            return View(a);
		}
		// GET: DeliveryDrivers/Edit/5
		public async Task<IActionResult> Edit(int? driverId ,int? orderId)
		{
			if (driverId == null ||orderId==null|| _context.Orders == null)
			{
				return NotFound();
			}

			var DeliveryRecords = await _context.Orders
                .Where(m=>m.DeliveryDriversId==driverId)
				.FirstOrDefaultAsync(o=>o.Id==orderId);
			
			if (DeliveryRecords == null)
			{
				return NotFound();
			}
		
			return View(DeliveryRecords);
		}

		// POST: DeliveryDrivers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,AccountStatusId,WorkStatuseId,FirstName,LastName,Phone,Gender,BankAccount,Idcard,RegistrationTime,VehicleRegistration,Birthday,Email,Account,Password,DriverLicense,Longitude,Latitude")] DeliveryDriver deliveryDriver)
		{
			if (id != deliveryDriver.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(deliveryDriver);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DeliveryDriverExists(deliveryDriver.Id))
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
			ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id", deliveryDriver.AccountStatusId);
			ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id", deliveryDriver.WorkStatuseId);
			return View(deliveryDriver);
		}
		private bool DeliveryDriverExists(int id)
		{
			return _context.DeliveryDrivers.Any(e => e.Id == id);
		}
	}
}
