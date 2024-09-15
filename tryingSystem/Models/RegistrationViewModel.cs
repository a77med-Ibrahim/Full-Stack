using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tryingSystem.Models

{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage ="First Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 50 charachters allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 50 charachters allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [MaxLength(50,ErrorMessage = "Max 100 charachters allowed.")]
        [EmailAddress(ErrorMessage ="Please Enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="User Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 20 charachters allowed.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MaxLength(50,ErrorMessage = "Max 20 charachters allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}