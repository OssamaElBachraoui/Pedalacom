using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class ModelDetails
    {
        [Key]
        public int ProductModelId { get; set; }
        public string? Model { get; set; }

        public List<GeneralProduct> GeneralProducts { get; set;} = new List<GeneralProduct>();
    }
}
