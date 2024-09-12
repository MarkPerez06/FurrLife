using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class CartProductsView
    {
        [Key]
        public int CartId { get; set; }
        public int? CartQuantity { get; set; }
        public DateTime? CartDateCreated { get; set; }
        public string? CartSessionId { get; set; }
        public string? MenuName { get; set; }
        public string? MenuDescription { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductImageURL { get; set; }
        public int? ProductRating { get; set; }
        public int? ProductDiscounts { get; set; }
        public string? UnitName { get; set; }
        public string? UnitCode { get; set; }
    }
}