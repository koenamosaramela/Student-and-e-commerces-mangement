using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace studentManagement_and__B2S__Consumer.Models
{
    public class Consumer : ApplicationUser
    {
        [StringLength(100)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [StringLength(100)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
