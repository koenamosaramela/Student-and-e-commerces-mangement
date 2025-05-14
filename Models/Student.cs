﻿

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


/*
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
* 
*/
namespace ASPNETCore_DB.Models
{
    public class Student
    {
        [Key]
        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Student Number is required.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "The Student Number may " +
            "only be 10 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Only digits are allowed")]
        public string? StudentNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The First Name may not be" +
            " shorter 2 characters or more than 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are allowed")]
        public string? FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Surname may not be" +
            " shorter 2 characters or more than 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are allowed")]
        public string? Surname { get; set; }

        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "Enrollment Date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Photo identicate")]
        public string? Photo {  get; set; }

        [Display(Name = "ContactFormModel mail")]
        [Required(ErrorMessage = "ContactFormModel Mail")]
        public string? Email {  get; set; }

    }
}
