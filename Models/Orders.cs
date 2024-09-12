using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Payment { get; set; }
        public string? CustomerRequest { get; set; }
        public decimal TotalAmount { get; set; }
        public bool? IsPaid { get; set; }
        public int Discounts { get; set; }
        public string? SeniorCitizenID { get; set; }
        public string? PWDID { get; set; }
        public string? UserId { get; set; }
        public DateTime DateCreated { get; set; }

    }
}