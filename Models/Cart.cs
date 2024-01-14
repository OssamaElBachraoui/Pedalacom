using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public int CustomerID { get; set; }

        public int ProductID { get; set; }
    }
}
