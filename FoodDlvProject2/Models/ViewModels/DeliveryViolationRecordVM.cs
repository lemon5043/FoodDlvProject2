namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryViolationRecordVM
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string Violation { get; set; }
        public string Content { get; set; }
        public DateTime ViolationDate { get; set; }
    }
}