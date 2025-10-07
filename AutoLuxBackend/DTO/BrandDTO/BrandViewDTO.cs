using AutoLuxBackend.DTO.CarDTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoLuxBackend.DTO.BrandDTO
{
    public class BrandViewDTO
    {
        //[Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CarViewDTO> Cars { get; set; } = new List<CarViewDTO>();
    }
}
