using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class ImmunizationHistory
    {
        public int Id { get; set; }

        public string Year { get; set; }

        public string Immunization { get; set; }
        public string Comments { get; set; }
    }

}
