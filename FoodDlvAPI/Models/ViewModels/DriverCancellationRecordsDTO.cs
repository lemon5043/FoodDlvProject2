namespace FoodDlvAPI.Models.ViewModels
{
    public class DriverCancellationRecordsDTO
    {
        public int Id { get; set; }
        public int CancellationId { get; set; }
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public DateTime CancellationDate { get; set; }
    }
    public static class DriverCancellationRecordsDTOExts
    {
        public static DriverCancellationRecord ToEFModel(this DriverCancellationRecordsDTO model)
        {
            return new DriverCancellationRecord
            {
                Id = model.Id,
                CancellationId = model.CancellationId,
                OrderId = model.OrderId,
                DeliveryDriversId = model.DriverId,
                CancellationDate = model.CancellationDate,
            };
        }
    }
}