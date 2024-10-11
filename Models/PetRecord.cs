using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class PetRecord
    {
        public int Id { get; set; }

        // Owner Information
        [Required]
        [MaxLength(100)]
        public string OwnerFullName { get; set; }

        [Required]
        [MaxLength(15)]
        public string OwnerContact { get; set; }

        [MaxLength(300)]
        public string OwnerAddress { get; set; }

        // Pet Information
        [Required]
        [MaxLength(100)]
        public string PetName { get; set; }

        public int Age { get; set; }

        [MaxLength(50)]
        public string Breed { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        public float Weight { get; set; }

        [MaxLength(500)]
        public string? PhysicalCondition { get; set; }

        // Medical History
        [MaxLength(1000)]
        public string? PreviousIllnessesOrSurgeries { get; set; }

        [MaxLength(500)]
        public string VaccinationStatus { get; set; }

        [MaxLength(500)]
        public string? CurrentMedicationsOrTreatments { get; set; }

        // Behavioral Observations
        [MaxLength(500)]
        public string? TemperamentAndPersonality { get; set; }

        [MaxLength(500)]
        public string? BehavioralIssuesOrChanges { get; set; }

        // Dietary Information
        [MaxLength(500)]
        public string? CurrentDietAndFeedingSchedule { get; set; }

        [MaxLength(500)]
        public string? FoodAllergiesOrSensitivities { get; set; }

        // Environment
        [MaxLength(500)]
        public string LivingConditions { get; set; }  // Indoor, outdoor, or both

        [MaxLength(500)]
        public string? InteractionWithOtherPets { get; set; }

        // Owner’s Concerns
        [MaxLength(1000)]
        public string? SpecificIssuesOrSymptoms { get; set; }

        [MaxLength(1000)]
        public string? OwnerQuestionsOrTopics { get; set; }

        // Routine Care
        [MaxLength(500)]
        public string? GroomingHabits { get; set; }

        [MaxLength(500)]
        public string? ExerciseRoutines { get; set; }

        // Consultation Details
        [MaxLength(500)]
        public string ReasonForVisit { get; set; }

        [MaxLength(500)]
        public string? Diagnosis { get; set; }

        [MaxLength(1000)]
        public string? TreatmentPlan { get; set; }

        [MaxLength(1000)]
        public string? FollowUpCareInstructions { get; set; }

        // Summary and Notes
        [MaxLength(1000)]
        public string ConsultationSummary { get; set; }

        public bool OwnerConsentGiven { get; set; }

        [MaxLength(500)]
        public string? ReferralInformation { get; set; }
    }

}
