using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace studentManagement_and__B2S__Consumer.Models
{
    public class Student : User
    {
        [Required]
        [Display(Name = "Student Number")]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("^[0-9]+$")]
        public string StudentNumber { get; set; }

        [Display(Name = "Course")]
        public string Course { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }

}
