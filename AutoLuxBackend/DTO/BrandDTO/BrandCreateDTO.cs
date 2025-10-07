using System.ComponentModel.DataAnnotations;

namespace AutoLuxBackend.DTO.BrandDTO
{
    public class BrandCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
