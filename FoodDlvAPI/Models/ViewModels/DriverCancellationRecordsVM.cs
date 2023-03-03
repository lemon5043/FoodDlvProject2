namespace FoodDlvAPI.Models.ViewModels
{
    public class DriverCancellationRecordsVM
    {
        public int Id { get; set; }
        public int CancellationId { get; set; }
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public DateTime CancellationDate => DateTime.UtcNow;
    }
    public static class DriverCancellationRecordsVMExts
    {
        public static DriverCancellationRecordsDTO ToDriverCancellationRecordsDTO(this DriverCancellationRecordsVM vm)
        {
            return new DriverCancellationRecordsDTO
            {
                CancellationId = vm.CancellationId,
                OrderId = vm.OrderId,
                DriverId = vm.DriverId,
                CancellationDate = vm.CancellationDate,
            };
        }
    }
}