using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class OldCustomer
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }

        public string? EmailAddress { get; set; }

        public int? IsOld { get; set; }
    }
}
