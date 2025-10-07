using AutoLuxBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLuxBackend.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task<Brand?> GetByNameAsync(string name);
        Task<Brand> AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(int id);
    }
}
