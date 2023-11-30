using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class PreviewProduct
    {
        [Key]
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int ProductCategoryID { get; set; }
     
       
    }
}
