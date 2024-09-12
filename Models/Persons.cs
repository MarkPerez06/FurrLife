using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrLife.Models
{
    public class Persons
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public string? CompleteAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? Gender { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsStaff { get; set; }
        public bool? IsMember { get; set; }
        public decimal Wallet { get; set; }
        public string? CardNumber { get; set; }
        public bool? IsSeniorCitizen { get; set; }
        public string? SeniorCitizenID { get; set; }
        public bool? IsPWD { get; set; }
        public string? PWDID { get; set; }
        public string? PIN { get; set; }
        public decimal? Salary { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserId { get; set; }
    }


    public class Users
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Password { get; set; }
    }
}