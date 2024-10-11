using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing;

namespace FurrLife.Models
{
    public class PetHealthRecord
    {
        public int Id { get; set; }

        //Pet Information
        public string PetName { get; set; }
        public string Age { get; set; }
        public DateTime Birthdate { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }

        //Behavioral Observations
        public string TemperamentAndPersonalityTraits { get; set; }
        public string BehavioralIssues { get; set; }

        //Routine Care:
        public string GroomingHabits { get; set; }
        public string ExerciseRoutines { get; set; }

        //Health Concerns
        public string Allergies { get; set; }
        public string FeedingSchedule { get; set; }
        public string ExistingConditions { get; set; }
        public string UserId { get; set; }


        //Owner's Information
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
