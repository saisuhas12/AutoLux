using AutoLuxBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLuxBackend.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Cars>> GetAllAsync();
        Task<Cars?> GetByIdAsync(int id);
        Task AddAsync(Cars car);
        Task UpdateAsync(Cars car);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Cars>> SearchCarsAsync(
            string? model, string? brand, int? year, decimal? price,
            int? mileage,
            string? color, bool? isSold);

    }
}
