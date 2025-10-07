using System.ComponentModel.DataAnnotations;

namespace AutoLuxBackend.DTO.CarDTO
{
    public class CarCreateDTO
    {
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

        public int? BrandId { get; set; }

        public string? BrandName { get; set; }
    }
}
