using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class DeliveryRecordDTO
    {
        public long Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Milage { get; set; }

        public decimal TotalMilage { get; set; }

        public int TotalDelievery { get; set; }

        public string Status { get; set; }

        public string DriverName { get; set; }

        public int DeliveryDriversId { get; set; }
    }
}
