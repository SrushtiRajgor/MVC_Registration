using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Registration1.Models
{
    public class Entities
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="First name is required!")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "First name can only contain letters and spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last name is required!")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Last name can only contain letters and spaces.")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress(ErrorMessage ="Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Birthdate is required!")]
        [DataType(DataType.Date, ErrorMessage ="Invalid date format.")]
        [CustomValidation(typeof(Entities), nameof(ValidateBirthdate))]
        public string Birthdate { get; set; }

        [Required(ErrorMessage ="Phone number is required!")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage ="Adress can not exceed 200 character.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "City can not exceed 50 character.")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "State can not exceed 50 character.")]
        public string State { get; set; }

        [Required (ErrorMessage ="Please enter a password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required (ErrorMessage ="Please Enter the Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Profile Image")]
        //public string ImagePath { get; set; }

        public static ValidationResult ValidateBirthdate(DateTime birthDate)
        {
            if(birthDate > DateTime.Now)
            
                return new ValidationResult("Birthdate cannot be in the future.");

            if (birthDate > DateTime.Now.AddYears(-18))
                return new ValidationResult("You must be at least 18 years old.");

            return ValidationResult.Success;
        }
    }
}
