using FoodDlvAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FoodDlvAPI.Controllers
{
    public class DelieveryController : Controller
    {
        private readonly DeliveryService deliveryService;

        private readonly IHubContext<OrderHub> _hubContext;

        public DelieveryController(IHubContext<OrderHub> hubContext)
        {
            var db = new AppDbContext();
            IDeliveryRepository repository = new DeliveryRepository(db);
            this.deliveryService = new DeliveryService(repository);
            this._hubContext = hubContext;
        }

        [HttpPost]
        public async Task Online(int dirverId)
        {
            await _hubContext.Clients.
        }

        [HttpPost]
        public async Task Offline(int dirverId)
        {

        }

        
        public async Task OrderAasignment(int OrderId)
        {

        }

        public async Task NavigationToStore()
        {

        }

        public async Task NavigationToDestination()
        {

        }

        public async Task DeliveryArrive()
        {

        }
    }
}
