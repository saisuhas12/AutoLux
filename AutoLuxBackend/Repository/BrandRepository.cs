using AutoLuxBackend.Context;
using AutoLuxBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoLuxBackend.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Brand>> GetAllAsync() =>
            await _context.Brands.Include(b => b.Cars).ToListAsync();

        public async Task<Brand?> GetByIdAsync(int id) =>
            await _context.Brands.Include(b => b.Cars).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Brand?> GetByNameAsync(string name) =>
            await _context.Brands.Include(b => b.Cars)
                                 .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());

        public async Task<Brand> AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return false;
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
