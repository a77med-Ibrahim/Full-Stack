using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tryingSystem.Entities;

namespace tryingSystem.Models
{
    public class TodoItemModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 chracters allowed.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public UserAccount? UserAccount{get; set;}
    }
}