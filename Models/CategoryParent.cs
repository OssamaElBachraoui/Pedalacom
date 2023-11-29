
using System.ComponentModel.DataAnnotations;
namespace Pedalacom.Models
{
    public class CategoryParent
    {
        
        [Key]
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }

    }

    
}
