using System.ComponentModel.DataAnnotations;

namespace StudentQueryAPI.Models
{
    public class StudentModel
    {
        [MaxLength(100)]
        public string StudentName { get; set; }

        [MaxLength(100)]
        public string StudentClass { get; set; }

        [MaxLength(100)]
        public string StudentAcademy { get; set; }

        [Range(0, 4.0)]
        public double StudentCPA { get; set; } = 0;
    }
}
