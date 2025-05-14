
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
* 
*/
namespace ASPNETCore_DB.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; } = "default-product.png";
        public int StockQuantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        // Relationships
        public int? CategoryId { get; set; }
        
       // public Category Category { get; set; }

        public ICollection<Promotion> Promotions { get; set; }
    }
}