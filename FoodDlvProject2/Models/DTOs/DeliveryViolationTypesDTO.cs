using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.DTOs
{
    public class DeliveryViolationTypesDTO
    {
        public int Id { get; set; }
        
        public string ViolationContent { get; set; }
        
        public string Content { get; set; }
    }

    public static class DeliveryViolationTypesExts
    {
        public static DeliveryViolationTypesDTO ToEntity(this DeliveryViolationType source)
        {
            return new DeliveryViolationTypesDTO
            {
                Id = source.Id,
                ViolationContent = source.ViolationContent,
                Content= source.Content,
            };
        }
    }
}