using System.ComponentModel.DataAnnotations;

namespace Api.ModelDto
{
    // модель для передачи данных при создании продукта
    public class ProductUpdateDto
    {
        public int Id { get; set; } // поле необязательно
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string SpecialTag { get; set; }
        public string Category { get; set; }
        [Range(1, 100_000)]
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
