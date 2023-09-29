using System.Collections.Generic;
using System.Threading.Tasks;
using Group3.Db;
using Group3.Reponsitory;
using Lib;
using Microsoft.EntityFrameworkCore;

namespace Group3.Services
{
    public class TypeService : IType
    {
        private readonly DatabaseContext _context;

        public TypeService(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<TypeMst> GetTypeByIdAsync(string id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task<IEnumerable<TypeMst>> GetAllTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task CreateTypeAsync(TypeMst type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTypeAsync(TypeMst type)
        {
            _context.Types.Update(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeAsync(string id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await _context.SaveChangesAsync();
            }
        }
    }
}
