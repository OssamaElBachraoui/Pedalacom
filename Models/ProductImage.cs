using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductId { get; set; }
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }
    }
}
