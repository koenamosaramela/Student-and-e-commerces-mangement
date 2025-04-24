
using System.ComponentModel.DataAnnotations;

namespace studentManagement_and__B2S__Consumer.Models
{
    // Models/Admin.cs
    public class Admin : User
    {
        [StringLength(50)]
        public string AdminCode { get; set; } // Example custom field

        [DataType(DataType.Date)]
        public DateTime? LastAuditDate { get; set; }
    }
}
