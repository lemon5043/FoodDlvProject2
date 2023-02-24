namespace FoodDlvAPI.Controllers
{
    internal class DeliveryService
    {
        private IDeliveryRepository repository;

        public DeliveryService(IDeliveryRepository repository)
        {
            this.repository = repository;
        }

        internal void ChangeToOffline(int dirverId)
        {
            throw new NotImplementedException();
        }

        internal void ChangeToOnline(int dirverId)
        {
            throw new NotImplementedException();
        }

        internal object GetOrderDetail(int orderId)
        {
            throw new NotImplementedException();
        }

        internal void NavigationToCustomer(int orderId)
        {
            throw new NotImplementedException();
        }

        internal void NavigationToStore(int orderId)
        {
            throw new NotImplementedException();
        }

        internal void OrderArrive(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}