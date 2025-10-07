using AutoLuxBackend.Context;
using AutoLuxBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoLuxBackend.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Cars>> GetAllAsync() =>
            await _context.Cars.Include(c => c.Brand).ToListAsync();

        public async Task<Cars?> GetByIdAsync(int id) =>
            await _context.Cars.Include(c => c.Brand).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Cars car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cars car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Cars>> SearchCarsAsync(string? model, string? brand, int? year,
             decimal? price, int? mileage, 
            string? color, bool? isSold)
        {
            var query = _context.Cars.Include(c => c.Brand).AsQueryable();
            if (!string.IsNullOrEmpty(model)) query = query.Where(c => c.Model.Contains(model));
            if (!string.IsNullOrEmpty(brand)) query = query.Where(c => c.Brand.Name.Contains(brand));
            if (year.HasValue) query = query.Where(c => c.Year == year);
            if (price.HasValue) query = query.Where(c => c.Price == price);
            if (mileage.HasValue) query = query.Where(c => c.Mileage == mileage);
            if (!string.IsNullOrEmpty(color)) query = query.Where(c => c.Color.Contains(color));
            if (isSold.HasValue) query = query.Where(c => c.IsSold == isSold);
            return await query.ToListAsync();
        }
    }
}
