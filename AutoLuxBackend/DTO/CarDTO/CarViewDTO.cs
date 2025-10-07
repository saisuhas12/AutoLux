using System.ComponentModel.DataAnnotations;

namespace AutoLuxBackend.DTO.CarDTO
{
    public class CarViewDTO
    {
        [Required]
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
        public int BrandId { get; set; }

        [Required]
        public string BrandName { get; set; } = string.Empty;
    }
}
