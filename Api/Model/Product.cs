using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Product
    {
        [Key] // первичный ключ (указание для EF)
        public int Id { get; set; }
        [Required] // обязательное поле
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string SpecialTag { get; set; }
        public string Category { get; set; }
        [Range(1, 100_000)] // разрешенный диапазон значений
        public double Price { get; set; }
        public string Image { get; set; }
    }
}