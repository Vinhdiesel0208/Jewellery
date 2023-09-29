using System.Collections.Generic;
using System.Threading.Tasks;
using Lib;

namespace Group3.Reponsitory
{
    public interface IGender
    {
        Task<Gender> GetGenderByIdAsync(string id);
        Task<IEnumerable<Gender>> GetAllGendersAsync();
        Task CreateGenderAsync(Gender gender);
        Task UpdateGenderAsync(Gender gender);
        Task DeleteGenderAsync(string id);
    }
}
