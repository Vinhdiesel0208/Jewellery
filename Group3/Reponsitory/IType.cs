using System.Collections.Generic;
using System.Threading.Tasks;
using Lib;

namespace Group3.Reponsitory
{
    public interface IType
    {
        Task<TypeMst> GetTypeByIdAsync(string id);
        Task<IEnumerable<TypeMst>> GetAllTypesAsync();
        Task CreateTypeAsync(TypeMst type);
        Task UpdateTypeAsync(TypeMst type);
        Task DeleteTypeAsync(string id);
    }
}
