using Group3.Models;
using Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group3.Reponsitory
{
    public interface ICat
    {
        Task<IEnumerable<CatMst>> GetAllAsync();
        Task<CatMst> GetByIdAsync(string id);
        Task CreateAsync(CatMst cat);
        Task UpdateAsync(CatMst cat);
        Task DeleteAsync(string id);
    }
}
