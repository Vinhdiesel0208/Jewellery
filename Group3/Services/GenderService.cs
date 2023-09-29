using System.Collections.Generic;
using System.Threading.Tasks;
using Group3.Db;
using Group3.Reponsitory;
using Lib;
using Microsoft.EntityFrameworkCore;

namespace Group3.Services
{
    public class GenderService : IGender
    {
        private readonly DatabaseContext _context;

        public GenderService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Gender> GetGenderByIdAsync(string id)
        {
            return await _context.Genders.FindAsync(id);
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task CreateGenderAsync(Gender gender)
        {
            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenderAsync(Gender gender)
        {
            _context.Genders.Update(gender);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenderAsync(string id)
        {
            var gender = await _context.Genders.FindAsync(id);
            if (gender != null)
            {
                _context.Genders.Remove(gender);
                await _context.SaveChangesAsync();
            }
        }
    }
}
