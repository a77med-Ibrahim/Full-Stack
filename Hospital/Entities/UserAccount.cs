using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tryingSystem.Models;

namespace tryingSystem.Entities
{
    [Index(nameof(Email), IsUnique =true)]
    [Index(nameof(UserName), IsUnique =true)]
    public class UserAccount
    {   
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 50 charachters allowed.")]
        public string ?FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 50 charachters allowed.")]
        public string ?LastName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [MaxLength(50,ErrorMessage = "Max 100 charachters allowed.")]
        public string ?Email { get; set; }
        [Required(ErrorMessage ="User Name is Required")]
        [MaxLength(50,ErrorMessage = "Max 20 charachters allowed.")]
        public string ?UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MaxLength(50,ErrorMessage = "Max 20 charachters allowed.")]
        public string ?Password { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string Role{get;set;} = "Nurse";
    }
}