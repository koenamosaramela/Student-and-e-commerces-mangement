﻿using System.ComponentModel.DataAnnotations;

/*
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
* 
*/
namespace ASPNETCore_DB.Models
{
    public class Consumer
    {
        [Key]
        [Display(Name = "Consumer ID")]
         public string? ConsumerId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "Registration Date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Profile Photo")]
        public string? Photo { get; set; }
    }
}