using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class Category
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public int ParentProductCategoryID { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        public List<PreviewProduct> models = new List<PreviewProduct>();
    }
}
