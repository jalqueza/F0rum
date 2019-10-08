using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // won't show char's
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Passowrd")] // name will have space between
        [Compare("Password", // compares both passwords
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}