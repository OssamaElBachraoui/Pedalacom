using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class GeneralProduct
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Color { get; set; }
        public decimal? ListPrice { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }
        public int ProductCategoryID { get; set; }
        public int ParentProductCategoryID { get; set; }

        public int ProductModelId {  get; set; }
        public string? Category { get; set; }
        public string? Model { get; set; }
        public int ProductDescriptionID { get; set; }
        public string? Description { get; set; }
    }



}
