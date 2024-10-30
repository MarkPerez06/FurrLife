using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Schedule
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; }
        public string UserId { get; set; }
    }
}