using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.Models.ViewModels
{
    public class ProductSelectionVM
    {
        [Display(Name = "商品編號")]
        public long ProductId { get; set; }

        [Display(Name = "店家編號")]
        public int StoreId { get; set; }

        [Display(Name = "商品名稱")]
        public string? ProductName { get; set; }

        [Display(Name = "商品圖像")]
        public string? Photo { get; set; }

        [Display(Name = "商品內容")]
        public string? ProductContent { get; set; }

        [Display(Name = "商品狀態")]
        public bool? Status { get; set; }

        [Display(Name = "商品單價")]
        public int UnitPrice { get; set; }

        public List<ProductCustomizationItemVM> CustomizationItems { get; set; }
    }

    public static partial class ProductSelectionVMExts
    {
        public static ProductSelectionVM ToProductSelectionVM(this ProductDTO source)
        {
            var toProductSelectionVM = new ProductSelectionVM
            {
                ProductId = source.ProductId,
                StoreId = source.StoreId,
                ProductName = source.ProductName,
                Photo = source.Photo,
                ProductContent = source.ProductContent,
                Status = source.Status,
                UnitPrice = source.UnitPrice,
                CustomizationItems = source.Items.Select(pci => new ProductCustomizationItemVM
                {
                    Id = pci.Id,
                    ProuctId = pci.ProuctId,
                    ItemName = pci.ItemName,
                    CustomizationItemPrice = pci.UnitPrice,
                }).ToList(),
            };
            return toProductSelectionVM;
        }
    }
}
