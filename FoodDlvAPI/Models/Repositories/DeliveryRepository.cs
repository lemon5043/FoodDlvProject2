using FoodDlvAPI.Models;

namespace FoodDlvAPI.Models.Repositories
{
    internal class DeliveryRepository
    {
        private AppDbContext db;

        public DeliveryRepository(AppDbContext db)
        {
            this.db = db;
        }
    }
}