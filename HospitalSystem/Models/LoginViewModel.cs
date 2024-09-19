using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]
        [DisplayName("Username or Email")]
        
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MaxLength(50,ErrorMessage = "Max 20 charachters allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}