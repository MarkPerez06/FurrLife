using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int AppointmentId { get; set; }
        public string? UserId { get; set; }
        public string? CustId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}