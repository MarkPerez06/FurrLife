using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public DateTime MedicationDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Diagnosis { get; set; }
        public string TestsPerformed { get; set; }
        public string TestResults { get; set; }
        public string Action { get; set; }
        public string Medication { get; set; }
        public string Comments { get; set; }
    }

}
