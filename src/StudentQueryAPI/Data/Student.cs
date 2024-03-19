using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentQueryAPI.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentClass { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentAcademy { get; set; }

        [Range(0, 4.0)]
        public double StudentCPA { get; set; } = 0;
    }
}
