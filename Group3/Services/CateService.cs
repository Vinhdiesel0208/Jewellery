using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Group3.Reponsitory;
using Microsoft.EntityFrameworkCore;
using Lib;
using Group3.Db;

namespace Group3.Services
{
    public class CatService : ICat
    {
        private readonly DatabaseContext _context;

        public CatService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<CatMst> GetByIdAsync(string id)
        {
            return await _context.CatMsts.FindAsync(id);
        }

        public async Task<IEnumerable<CatMst>> GetAllAsync()
        {
            return await _context.CatMsts.ToListAsync();
        }

        public async Task CreateAsync(CatMst cat)
        {
            _context.CatMsts.Add(cat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CatMst cat)
        {
            _context.CatMsts.Update(cat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var cat = await _context.CatMsts.FindAsync(id);
            if (cat != null)
            {
                _context.CatMsts.Remove(cat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
