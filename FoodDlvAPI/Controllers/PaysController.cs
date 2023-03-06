using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : Controller
    {
        private readonly PaysService paysService;

        public PaysController()
        {
            var db = new AppDbContext();
            IPaysRepository repository = new PaysRepository(db);
            this.paysService = new PaysService(repository);
        }

        [HttpGet("{id}")]
        // GET: Pays/IndividualMonthly/5
        public async Task<IEnumerable<PaysMonthlyDetailsVM>> MonthlyDetails(int? id)
        {
            try
            {
                var data = await paysService.GetMonthlyDetailsAsync(id);
                return data.Select(x => x.ToPaysMonthlyDetailsVM());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{year}/{month}/{id}")]
        // GET: Pays/IndividualMonthly/5
        public async Task<ActionResult<PaysIndividualMonthlyDetailsVM>> IndividualMonthlyDetails(int? year, int? month, int? id)
        {
            try
            {
                var data = await paysService.GetIndividualMonthlyDetailsAsync(year, month, id);
                return data.ToPaysIndividualMonthlyDetailsVM();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
