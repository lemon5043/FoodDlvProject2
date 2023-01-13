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
            var appDbContext = _context.Orders.Include(d => d.DeliveryDrivers);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryRecords/MonthlyDetails/5
        public async Task<IActionResult> MonthlyDetails(int? id)
        {
            if (id == null || _context.Orders == null)
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
                }).ToListAsync();

            var a = DeliveryRecords.GroupBy(r => r.Id)
            .Select(g => g.OrderByDescending(r => r.MarkTime)
            .FirstOrDefault())
            .Select(s => new
            {
                Id = s.Id,
                OrderDate = s.MarkTime,
                Milage = s.Milage,
            })
            .GroupBy(r => r.OrderDate.Month).ToList()
            .Select(s => new DeliveryMonthlyDetailRecordVM
            {
                TotalMilage = s.Sum(x => x.Milage),
                TotalDelievery = s.Count(),
                OrderDate = s.Min(x => x.OrderDate),
            })
            ;
            if (DeliveryRecords == null)
            {
                return NotFound();
            }
            ViewBag.DriverId = id;

            return View(a);
        }
        // GET: DeliveryRecords/IndividualMonthlyDetails/5
        public async Task<IActionResult> IndividualMonthlyDetails(int? year,int? month,int? id)
        {
            if ( id == null || month==null || year==null || _context.Orders == null)
            {
                return NotFound();
            }

            var DeliveryRecords = await _context.Orders
                .Include(d => d.DeliveryDrivers)
                .Where(m => m.DeliveryDriversId == id)
                .Join(_context.OrderSchedules.Include(x=>x.Status).Where(s => s.StatusId > 3), o => o.Id, s => s.OrderId, (o, s) => new
                {
                    o.Id,
                    o.Milage,
                    s.MarkTime,
                    s.Status.Status,
                }).ToListAsync();

            var a = DeliveryRecords.GroupBy(r => r.Id)
            .Select(g => g.OrderByDescending(r => r.MarkTime)
            .FirstOrDefault())
            .Where(x => x.MarkTime.Year == year && x.MarkTime.Month == month)
            .Select(s => new DeliveryIndividualDetailsRecordVM
            {
                Id = s.Id,
                OrderDate = s.MarkTime,
                Milage = s.Milage,
                Status = s.Status.ToString(),
            });

            
            if (DeliveryRecords == null)
            {
                return NotFound();
            }
            ViewBag.Year = year;
			ViewBag.Month = month;
			ViewBag.DriverId = id;

			return View(a);
        }
       
    }
}
