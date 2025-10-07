using AutoMapper;
using AutoLuxBackend.Models;
using AutoLuxBackend.DTO.BrandDTO;

namespace AutoLuxBackend.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandViewDTO>();
            CreateMap<BrandCreateDTO, Brand>();
        }
    }
}
