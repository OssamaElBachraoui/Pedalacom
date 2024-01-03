using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class FilterAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AdrressLine2 {  get; set; }
        public string City { get; set; } = null!;
        public string StateProvince { get; set; } = null!;

    }
}
