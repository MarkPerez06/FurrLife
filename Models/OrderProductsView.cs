using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class OrderProductsView
    {
        [Key]
        public int OrderId { get; set; }
        public int? OrderQuantity { get; set; }
        public decimal? OrderPrice { get; set; }
        public int? OrderDiscounts { get; set; }
        public string? OrderUserId { get; set; }
        public string? MenuName { get; set; }
        public string? MenuDescription { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImageURL { get; set; }
        public int? ProductRating { get; set; }
        public string? UnitName { get; set; }
        public string? UnitCode { get; set; }
    }


    //public class CartProducts
    //{
    //    public int Id { get; set; }
    //    public int? ProductId { get; set; }
    //    public int? Quantity { get; set; }
    //    public DateTime? DateCreated { get; set; }
    //    public string SessionId { get; set; }
    //}


    //public class OrderProducts
    //{
    //    public int Id { get; set; }
    //    public int? Quantity { get; set; }
    //    public int? OrderId { get; set; }
    //    public int? ProductId { get; set; }
    //    public decimal? Price { get; set; }
    //    public int? Discounts { get; set; }
    //    public string UserId { get; set; }
    //}
}