using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public int Discounts { get; set; }
        public int ProductRating { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UnitId { get; set; }
        public int? MenuId { get; set; }
    }
}