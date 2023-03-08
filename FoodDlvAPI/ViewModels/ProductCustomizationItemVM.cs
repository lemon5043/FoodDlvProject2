using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvAPI.ViewModels
{
    public class ProductCustomizationItemVM
    {
        [Display(Name = "客製化編號")]
        public int Id { get; set; }

        [Display(Name = "產品編號")]
        public long ProuctId { get; set; }

        [Display(Name = "客製化內容")]
        public string? ItemName { get; set; }

        [Display(Name = "客製化價格")]
        public int CustomizationItemPrice { get; set; }       
    }
}
