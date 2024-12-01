using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; }
        public string UserId { get; set; }
        public string? CusUserId { get; set; }
        public int OrderId { get; set; }
    }
}