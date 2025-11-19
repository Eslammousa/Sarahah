using sarahah.Core.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;



namespace sarahah.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Please write your Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Please write your Email Address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "{0} is already taken .")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please write your Phone Number")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Should contain only numbers")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Please select your Gender")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid Gender")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Password can't be Empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password don't match")]
        public string ConfirmPassword { get; set; }  = null!;

    }
}
