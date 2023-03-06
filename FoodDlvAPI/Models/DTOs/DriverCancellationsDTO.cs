using FoodDlvAPI.Models.ViewModels;

namespace FoodDlvAPI.Models.DTOs
{
    public class DriverCancellationsDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Content { get; set; }
    }
    public static class DriverCancellationsDTOExts
    {
        public static DriverCancellationsVM ToDriverCancellationsVM(this DriverCancellationsDTO Dto)
        {
            return new DriverCancellationsVM
            {
                Id = Dto.Id,
                Reason = Dto.Reason,
                Content = Dto.Content,
            };
        }
    }
}