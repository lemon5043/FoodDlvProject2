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

        public void NavigationToStore(int orderId)
        {
            throw new NotImplementedException();
        }


        public async Task<string> NavigationToCustomer(int orderId)
        {
            var query = _repository.GetOrderDetail(orderId);
            string token = "AIzaSyDgJoRvP0mMm-RCym6eWkNH95pWuY1xUlk";
            string start = "25.0574121,121.5964832";
            string end = "25.0534121,121.5964832";
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
        public void OrderArrive(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}