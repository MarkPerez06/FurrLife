using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing;
using System.ComponentModel;

namespace FurrLife.Models
{
    public class PetHealthRecord
    {
        public int Id { get; set; }

        //Pet Information
        [DisplayName("Pet Name")]
        public string PetName { get; set; }
        [DisplayName("Age")]
        public string Age { get; set; }
        [DisplayName("Birth Date")]
        public DateTime Birthdate { get; set; }
        [DisplayName("Breed")]
        public string Breed { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set; }
        [DisplayName("Color")]
        public string Color { get; set; }

        //Behavioral Observations
        [DisplayName("Temperament and Personality Traits")]
        public string TemperamentAndPersonalityTraits { get; set; }
        [DisplayName("Behavioral Issues")]
        public string BehavioralIssues { get; set; }

        //Routine Care:
        [DisplayName("Grooming Habits")]
        public string GroomingHabits { get; set; }
        [DisplayName("Exercise Routines")]
        public string ExerciseRoutines { get; set; }

        //Health Concerns
        [DisplayName("Allergies")]
        public string Allergies { get; set; }
        [DisplayName("Feeding Schedule")]
        public string FeedingSchedule { get; set; }
        [DisplayName("Existing Conditions")]
        public string ExistingConditions { get; set; }
        [DisplayName("Veterinarian")]
        public string UserId { get; set; }


        //Owner's Information
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Complete Address")]
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
