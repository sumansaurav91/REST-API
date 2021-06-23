using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOS
{
    public class UpdateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,2000)]
        public decimal Price { get; set; }
    }
}