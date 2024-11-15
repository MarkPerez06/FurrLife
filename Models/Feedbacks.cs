using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Feedbacks
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public string UserId { get; set; }
        [DisplayName("Show to Shop Page")]
        public bool IsShow { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}