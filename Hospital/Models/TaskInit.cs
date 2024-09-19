using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tryingSystem.Entities;

namespace tryingSystem.Models
{
    public class TaskInit
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public int AssignedToNurseId { get; set; }

        [ForeignKey("AssingedToNurseId")]
        public UserAccount AssignedToNurse { get; set; }
        [Required]
        public int AssignedByDoctorId { get; set; }
        [ForeignKey("AssingedByDcotorId")]
        public UserAccount AssignedByDoctor { get; set; }

    }
}