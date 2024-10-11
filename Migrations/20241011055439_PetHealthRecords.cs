using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurrLife.Migrations
{
    public partial class PetHealthRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetRecord");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmunizationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Immunization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmunizationHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestsPerformed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetHealthRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemperamentAndPersonalityTraits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BehavioralIssues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroomingHabits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseRoutines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedingSchedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExistingConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetHealthRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ImmunizationHistory");

            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropTable(
                name: "PetHealthRecord");

            migrationBuilder.CreateTable(
                name: "PetRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    BehavioralIssuesOrChanges = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConsultationSummary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CurrentDietAndFeedingSchedule = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CurrentMedicationsOrTreatments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExerciseRoutines = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FollowUpCareInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FoodAllergiesOrSensitivities = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GroomingHabits = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InteractionWithOtherPets = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LivingConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OwnerAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OwnerConsentGiven = table.Column<bool>(type: "bit", nullable: false),
                    OwnerContact = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerQuestionsOrTopics = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhysicalCondition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PreviousIllnessesOrSurgeries = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReferralInformation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SpecificIssuesOrSymptoms = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TemperamentAndPersonality = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VaccinationStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetRecord", x => x.Id);
                });
        }
    }
}
