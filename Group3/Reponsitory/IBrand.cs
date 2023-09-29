using Group3.Models;
using Lib;

namespace Group3.Reponsitory
{
    public interface IBrand
    {
        Task<BrandMst> GetByIdAsync(string id);
        Task<IEnumerable<BrandMst>> GetAllAsync();
        Task CreateAsync(BrandMst brand);
        Task UpdateAsync(BrandMst brand);
        Task DeleteAsync(string id);
    }
}
