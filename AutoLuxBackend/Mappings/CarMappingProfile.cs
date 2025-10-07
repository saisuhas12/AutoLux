using AutoMapper;
using AutoLuxBackend.Models;
using AutoLuxBackend.DTO.CarDTO;

namespace AutoLuxBackend.Mappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarCreateDTO, Cars>();
            CreateMap<Cars, CarViewDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
        }
    }
}
