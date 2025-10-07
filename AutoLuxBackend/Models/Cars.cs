using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLuxBackend.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public bool IsSold { get; set; }

        [Required]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
