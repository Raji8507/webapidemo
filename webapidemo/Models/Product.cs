using System.ComponentModel.DataAnnotations;

namespace webapidemo.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public int CatId { get; set; }

        [Range(1, 100000.00)]
        public decimal ProductPrice { get; set; }

        public bool IsAvailable { get; set; }

        public Category? Category { get; set; }
    }
}