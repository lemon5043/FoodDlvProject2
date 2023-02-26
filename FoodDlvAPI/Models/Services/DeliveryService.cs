using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.ViewModels;
using NuGet.Protocol;
using System.Net;

namespace FoodDlvAPI.Models.Services
{
    public class DeliveryService
    {
        private readonly IDeliveryRepository _repository;

        public DeliveryService(IDeliveryRepository repository)
        {
            _repository = repository;
        }

        public void ChangeWorkingStatus(int dirverId)
            => _repository.ChangeWorkingStatus(dirverId);

        //public void ChangeToOffline(int dirverId)
        //    => _repository.ChangeToOffline(dirverId);

        public async Task<AasignmentOrderDTO> GetOrderDetail(int orderId)
            => await _repository.GetOrderDetail(orderId);

        public async Task MarkOrderStatus(int orderId)
            => await _repository.MarkOrderStatus(orderId);

        public async Task<AasignmentOrderDTO> NavigationToStore(int orderId)
            => await _repository.NavigationToStore(orderId);


        public async Task<string> NavigationToCustomer(int orderId)
        {
            var query = await _repository.NavigationToCustomer(orderId);
            string token = "AIzaSyDgJoRvP0mMm-RCym6eWkNH95pWuY1xUlk";
            string start = query.StoreAddress;
            string end = query.DeliveryAddress;
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/directions/json?language=zh-TW&origin=" + start + "&destination=" + end + "&key=" + token);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);          
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
          
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

    }
}