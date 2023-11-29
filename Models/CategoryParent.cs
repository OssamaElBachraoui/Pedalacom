
using System.ComponentModel.DataAnnotations;
namespace Pedalacom.Models
{
    public class CategoryParent
    {
        [Key]
        public int ParentProductCategoryId { get; set; }
        public string Name { get; set; }

        public List<Category> ChildCategories= new List<Category>();

    }

    public class Category {

        [Key]
        public int CategoryId { get; set;}

        public int ParentProductCategoryId { get; set; }
        public string Name { get; set; }

        public List <GeneralProduct> GeneralProducts = new List<GeneralProduct>();

    }
}
