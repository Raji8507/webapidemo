using System.ComponentModel.DataAnnotations;
using webapidemo.Models;

namespace webapidemo.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public List<Product>? Products{ get; set; }
    }
}