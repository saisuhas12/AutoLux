using AutoMapper;
using AutoLuxBackend.Models;
using AutoLuxBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoLuxBackend.DTO.CarDTO;

namespace AutoLuxBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepo;
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepo, IBrandRepository brandRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCarAsync([FromBody] CarCreateDTO dto)
        {
            Brand? brand = null;
            if (dto.BrandId.HasValue)
            {
                brand = await _brandRepo.GetByIdAsync(dto.BrandId.Value);
                if (brand == null) return BadRequest("BrandId not found");
            }
            else if (!string.IsNullOrWhiteSpace(dto.BrandName))
            {
                brand = await _brandRepo.GetByNameAsync(dto.BrandName);
                if (brand == null)
                {
                    brand = new Brand { Name = dto.BrandName };
                    await _brandRepo.AddAsync(brand);
                }
            }
            else
            {
                return BadRequest("BrandId or BrandName must be provided");
            }

            var car = _mapper.Map<Cars>(dto);
            car.BrandId = brand.Id;
            await _carRepo.AddAsync(car);

            var result = _mapper.Map<CarViewDTO>(car);
            result.BrandName = brand.Name;
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await _carRepo.GetAllAsync();
            var carDtos = cars.Select(_mapper.Map<CarViewDTO>).ToList();
            return Ok(carDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var car = await _carRepo.GetByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(_mapper.Map<CarViewDTO>(car));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarAsync(int id, [FromBody] CarCreateDTO updatedCar)
        {
            var car = await _carRepo.GetByIdAsync(id);
            if (car == null) return NotFound();

            _mapper.Map(updatedCar, car);
            if (updatedCar.BrandId.HasValue)
                car.BrandId = updatedCar.BrandId.Value;
            await _carRepo.UpdateAsync(car);
            return Ok(_mapper.Map<CarViewDTO>(car));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCarAsync(int id)
        //{
        //    var deleted = await _carRepo.DeleteAsync(id);
        //    return deleted ? Ok($"Deleted car with ID {id}") : NotFound($"Car ID {id} not found");
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            var deleted = await _carRepo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchCars(
        [FromQuery] string? model,
        [FromQuery] string? brand,
        [FromQuery] int? year,
        [FromQuery] decimal? price,
        [FromQuery] int? mileage,
        [FromQuery] string? color,
        [FromQuery] bool? isSold)
        {
            var cars = await _carRepo.SearchCarsAsync(model, brand, year, price, mileage, color, isSold);
            var carDtos = cars.Select(_mapper.Map<CarViewDTO>).ToList();
            return Ok(carDtos);
        }

    }
}
