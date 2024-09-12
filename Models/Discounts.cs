using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Discounts
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Percentage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}