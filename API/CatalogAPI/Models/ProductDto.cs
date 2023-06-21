using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
