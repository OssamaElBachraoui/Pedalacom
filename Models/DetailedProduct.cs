using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class DetailedProduct
    {
        [Key]
        public int ProductID { get; set; }
        public string? product { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }
        public decimal? ListPrice { get; set; }
        public string? Description { get; set; }
    }



}
