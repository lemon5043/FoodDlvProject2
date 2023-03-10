using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodDlvAPI.Models.DTOs
{
    public class MemberAccountAddressDto
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
    public static class MemberAccountAddressDtoExts
    {
        public static  AccountAddress ToEFmodel(this MemberAccountAddressDto source)
        {
            return new AccountAddress
            {
                Id = source.Id,
                Address = source.Address,
                MemberId = source.MemberId,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
            };
        }
       
    }
    
}
