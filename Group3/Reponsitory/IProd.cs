using Lib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group3.Reponsitory
{
    public interface IProd
    {
        Task<IEnumerable<ProdMst>> GetAllAsync();
        Task<ProdMst> GetByIdAsync(string id);
        Task CreateAsync(ProdMst prod, List<IFormFile> imageFiles);
        Task UpdateAsync(ProdMst prod, List<IFormFile> imageFiles);
        Task DeleteAsync(string id);
    }
}