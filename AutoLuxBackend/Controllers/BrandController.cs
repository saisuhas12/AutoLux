using AutoMapper;
using AutoLuxBackend.Models;
using AutoLuxBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoLuxBackend.DTO.BrandDTO;

namespace AutoLuxBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        // Create a new brand
        [HttpPost("add")]
        public async Task<IActionResult> CreateAsync([FromBody] BrandCreateDTO brandModel)
        {
            var existing = await _brandRepo.GetByNameAsync(brandModel.Name);
            if (existing != null)
                return BadRequest("Brand already exists!");

            var brand = _mapper.Map<Brand>(brandModel);
            var result = await _brandRepo.AddAsync(brand);
            return Ok(_mapper.Map<BrandViewDTO>(result));
        }

        // Get all brands
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            var brandDtos = brands.Select(_mapper.Map<BrandViewDTO>).ToList();
            return Ok(brandDtos);
        }

        // Get a brand by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null)
                return NotFound();

            return Ok(_mapper.Map<BrandViewDTO>(brand));
        }

        // Update an existing brand
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] BrandCreateDTO updatedBrand)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null)
                return NotFound();

            brand.Name = updatedBrand.Name;
            await _brandRepo.UpdateAsync(brand);
            return Ok(_mapper.Map<BrandViewDTO>(brand));
        }

        // Delete a brand
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var deleted = await _brandRepo.DeleteAsync(id);
        //    return deleted
        //        ? Ok($"Deleted Brand with ID {id}")
        //        : NotFound($"Brand ID {id} not found");
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _brandRepo.DeleteAsync(id);
            return deleted
                ? Ok(new { message = $"Deleted Brand with ID {id}" })
                : NotFound(new { message = $"Brand ID {id} not found" });
        }
    }
}
