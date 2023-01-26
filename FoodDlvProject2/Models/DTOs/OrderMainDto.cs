using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class OrderMainDto
    {
		public int Revenue { get; set; }
				
		public int CompletedOrder { get; set; }
				
		public int ExceptionOrder { get; set; }
				
		public int InProcessingOrder { get; set; }
	}
}
