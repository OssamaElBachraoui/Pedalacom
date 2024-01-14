using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public string EmailAddress { get; set; }

        public int ProductID { get; set; }

        public string Name { get; set; } = null!;
        public decimal ListPrice { get; set; }

        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

    }
}
