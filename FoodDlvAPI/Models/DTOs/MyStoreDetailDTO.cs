using FoodDlvAPI.Models;

namespace FoodDlvAPI.Models.DTOs
{
    public class MyStoreDetailDTO
    {
        public int Id { get; set; }
        public int StorePrincipalId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Photo { get; set; }



        public IEnumerable<string> CategoryName { get; set; }



        public IEnumerable<long> ProductId { get; set; }
        public IEnumerable<string> ProductName { get; set; }
        public IEnumerable<string> ProductPhoto { get; set; }
        public IEnumerable<string> ProductContent { get; set; }
        public IEnumerable<bool?> ProductStatus { get; set; }
        public IEnumerable<int> ProductUnitPrice { get; set; }

    }
}
