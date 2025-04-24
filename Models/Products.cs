using System.ComponentModel.DataAnnotations;

namespace studentManagement_and__B2S__Consumer.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
